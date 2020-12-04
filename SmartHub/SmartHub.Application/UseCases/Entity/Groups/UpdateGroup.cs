using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.SignalR.Services;
using SmartHub.Domain;
using SmartHub.Domain.Common;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
    public record GroupUpdateCommand : IRequest<Response<GroupDto>>
    {
        public string Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
    }

    public class GroupUpdateHandler : IRequestHandler<GroupUpdateCommand, Response<GroupDto>>
    {
		private readonly IOptionsSnapshot<AppConfig> _appConfig;
		private readonly IBaseRepositoryAsync<Group> _groupRepository;
		private readonly ISendOverSignalR _sendOverSignalR;
		public GroupUpdateHandler(IOptionsSnapshot<AppConfig> appConfig, IBaseRepositoryAsync<Group> groupRepository, ISendOverSignalR sendOverSignalR)
		{
			_appConfig = appConfig;
			_groupRepository = groupRepository;
			_sendOverSignalR = sendOverSignalR;
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

			var foundGroup = await _groupRepository.FindByAsync(x => x.Id == request.Id);
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

			await _sendOverSignalR.SendGroups();
			return Response.Ok($"Updated group with name {request.Name}", new GroupDto());
        }
    }
}