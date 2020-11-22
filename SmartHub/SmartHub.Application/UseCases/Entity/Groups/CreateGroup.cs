using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.SignalR.Services;
using SmartHub.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Groups
{
	public class GroupCreateCommand : IRequest<Response<string>>
	{
		public string Name { get; }
		public string Description { get; }
		public bool IsSubGroup { get; }
		public string? ParentGroupId { get; }

		public GroupCreateCommand(string description, string name, string? parentGroupId, bool isSubGroup = default)
		{
			Description = description;
			Name = name;
			IsSubGroup = isSubGroup;
			ParentGroupId = parentGroupId;
		}
	}

	public class GroupCreateHandler : IRequestHandler<GroupCreateCommand, Response<string>>
	{
		private readonly IBaseRepositoryAsync<Group> _groupRepository;

		public GroupCreateHandler(IBaseRepositoryAsync<Group> groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<Response<string>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
		{
			if (request.IsSubGroup && !string.IsNullOrEmpty(request.ParentGroupId))
			{
				var foundGroup = await _groupRepository.FindbyAsync(x => x.Id == request.ParentGroupId);
				if (foundGroup is not null && foundGroup.IsSubGroup)
				{
					return Response.Fail("You can not create a subgroup of a subgroup.", "");
				}
				foundGroup?.AddSubGroup(new Group(request.Name, request.Description, request.IsSubGroup));
				return Response.Ok($"Created new SubGroup with name {request.Name} for group {foundGroup?.Name}.");
			}

			var created = await _groupRepository.AddAsync(new Group(request.Name, request.Description));
			return created
				? Response.Ok($"Created new Group with name {request.Name}.")
				: Response.Fail($" Couldn't create new Group with name {request.Name}", "");
		}
	}
}