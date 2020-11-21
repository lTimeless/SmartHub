using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.SignalR.Services;
using SmartHub.Domain.Common;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices.Create
{
    public class DeviceCreateHandler : IRequestHandler<DeviceCreateCommand, Response<string>>
    {
		private readonly IBaseRepositoryAsync<Device> _deviceRepositry;
		private readonly ISendOverSignalR _sendOverSignalR;
		public DeviceCreateHandler(IBaseRepositoryAsync<Device> deviceRepositry, ISendOverSignalR sendOverSignalR)
		{
			_deviceRepositry = deviceRepositry;
			_sendOverSignalR = sendOverSignalR;
		}

		public async Task<Response<string>> Handle(DeviceCreateCommand request, CancellationToken cancellationToken)
        {
			var foundDevice = await _deviceRepositry.FindbyAsync(x => x.Name == x.Name);
			if (foundDevice is not null)
			{
				return Response.Fail($"Device with name {request.Name} already exists", "");

			}

			var newDevice = new Device(request.Name, request.Description, request.Ipv4, request.CompanyName,
                request.PrimaryConnection, request.SecondaryConnection,
                request.PluginName, request.PluginTypes);

            if (string.IsNullOrEmpty(request.GroupName))
            {
                request.GroupName = DefaultNames.DefaultGroup;
            }

			var created = await _deviceRepositry.AddAsync(newDevice);
			await _sendOverSignalR.SendDevices();
			return created
				? Response.Ok($"Created new Device with name {newDevice.Name}")
				: Response.Fail($" Couldn't create new Device with name {newDevice.Name}", "");
		}
	}
}