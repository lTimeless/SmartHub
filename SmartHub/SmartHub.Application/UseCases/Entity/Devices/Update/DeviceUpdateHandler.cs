using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.SignalR.Services;
using SmartHub.Domain;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices.Update
{
    public class DeviceUpdateHandler : IRequestHandler<DeviceUpdateCommand, Response<string>>
    {
		private readonly IOptionsSnapshot<AppConfig> _appConfig;
		private readonly IBaseRepositoryAsync<Device> _deviceRepository;
		private readonly ISendOverSignalR _sendOverSignalR;
		public DeviceUpdateHandler(IOptionsSnapshot<AppConfig> appConfig, IBaseRepositoryAsync<Device> deviceRepository, ISendOverSignalR sendOverSignalR)
		{
			_appConfig = appConfig;
			_deviceRepository = deviceRepository;
			_sendOverSignalR = sendOverSignalR;
		}

		public async Task<Response<string>> Handle(DeviceUpdateCommand request, CancellationToken cancellationToken)
        {
            if (_appConfig.Value.IsActive is false)
            {
                return Response.Fail("Error: No home created yet.", string.Empty);
            }
			var foundDevice = await _deviceRepository.FindbyAsync(x => x.Id == request.Id);
			if (foundDevice is null)
			{
				return Response.Fail($"Error: Couldn't find device with id {request.Id}.", string.Empty);
			}

			if (!string.IsNullOrEmpty(request.Name))
			{
				foundDevice.SetName(request.Name);
			}
			if (!string.IsNullOrEmpty(request.Description))
			{
				foundDevice.SetDescription(request.Description);
			}
			if (!string.IsNullOrEmpty(request.Ipv4))
			{
				foundDevice.SetIp(request.Ipv4);
			}
			foundDevice.SetConnectionTypes(request.PrimaryConnection, request.SecondaryConnection);
			await _sendOverSignalR.SendDevices();
			return Response.Ok($"Updated device with name {request.Name}");
        }
    }
}