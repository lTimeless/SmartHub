using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain;
using SmartHub.Domain.Common;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
    public class GroupUpdateCommand : IRequest<Response<GroupDto>>
    {
        public string Id { get; }
        public string? Name { get; }
        public string? Description { get; }

        public GroupUpdateCommand(string? description, string? name, string id)
        {
            Description = description;
            Name = name;
            Id = id;
        }
    }

    public class GroupUpdateHandler : IRequestHandler<GroupUpdateCommand, Response<GroupDto>>
    {
		private readonly IOptionsSnapshot<AppConfig> _appConfig;
		private readonly IBaseRepositoryAsync<Group> _groupRepository;
		public GroupUpdateHandler(IOptionsSnapshot<AppConfig> appConfig, IBaseRepositoryAsync<Group> groupRepository)
		{
			_appConfig = appConfig;
			_groupRepository = groupRepository;
		}

		public async Task<Response<GroupDto>> Handle(GroupUpdateCommand request, CancellationToken cancellationToken)
        {
            if (_appConfig.Value.IsActive is false)
            {
                return Response.Fail("Error: No home created yet.", new GroupDto());
            }

            if (request.Name == DefaultNames.DefaultGroup)
            {
                return Response.Fail("Error: You can't rename the default group.", new GroupDto());
            }

			var foundGroup = await _groupRepository.FindbyAsync(x => x.Id == request.Id);
			if (foundGroup is null)
			{
				return Response.Fail($"Error: Couldn't find group with id {request.Id}.", new GroupDto());
			}

			if (!string.IsNullOrEmpty(request.Name))
			{
				foundGroup.SetName(request.Name);
			}
			if (!string.IsNullOrEmpty(request.Description))
			{
				foundGroup.SetDescription(request.Description);
			}

 
            return Response.Ok($"Updated group with name {request.Name}", new GroupDto());
        }
    }
}