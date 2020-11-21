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
		private readonly ISendOverSignalR _sendOverSignalR;

		public GroupCreateHandler(IBaseRepositoryAsync<Group> groupRepository, ISendOverSignalR sendOverSignalR)
		{
			_groupRepository = groupRepository;
			_sendOverSignalR = sendOverSignalR;
		}

		public async Task<Response<string>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
		{
			var groups = await _groupRepository.GetAllAsync();

			if (request.IsSubGroup && !string.IsNullOrEmpty(request.ParentGroupId))
			{
				var foundGroup = groups.Find(x => x.Id == request.ParentGroupId);
				if (foundGroup is not null && foundGroup.IsSubGroup)
				{
					return Response.Ok("You can not create a subgroup of a subgroup.");
				}
				foundGroup?.AddSubGroup(new Group(request.Name, request.Description, request.IsSubGroup));
				return Response.Ok($"Created new SubGroup with name {request.Name} for group {foundGroup?.Name}.");
			}
			groups.Add(new Group(request.Name, request.Description));
			await _sendOverSignalR.SendGroups();
			return Response.Ok($"Created new Group with name {request.Name}.");
		}
	}
}