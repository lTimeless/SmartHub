using MediatR;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateHandler : IRequestHandler<DeviceLightStateQuery, ServiceResponse<DeviceStateDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginHostService _pluginHostService;
		private readonly IHttpService _httpService;

		public DeviceLightStateHandler(IUnitOfWork unitOfWork, IPluginHostService pluginHostService, IHttpService httpService)
		{
			_unitOfWork = unitOfWork;
			_pluginHostService = pluginHostService;
			_httpService = httpService;
		}

		public async Task<ServiceResponse<DeviceStateDto>> Handle(DeviceLightStateQuery request, CancellationToken cancellationToken)
		{
			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}
			var home = await _unitOfWork.HomeRepository.GetFirstAsync();
			var foundDevice = home.Devices.Find(x => x.Id == request.LightStateDto.DeviceId);
			if (foundDevice is null)
			{
				throw new SmartHubException($"[{nameof(DeviceLightStateHandler)}] Error: No device found to the given deviceId {request.LightStateDto.DeviceId}");
			}
			var dbPlugin = home.Plugins.Find(x => x.Name.Equals(foundDevice.PluginName));
			if (dbPlugin is null)
			{
				throw new DeviceStateException($"[{nameof(DeviceLightStateHandler)}] Error: No plugin found to the device {foundDevice.Name}");
			}
			var pluginObject = await _pluginHostService.LightPlugins.GetIPluginByName(dbPlugin.Name);
			var query = "";
			if (pluginObject.HttpSupport)
			{
				query = pluginObject.Instantiate()
					.SetToggleLight(request.LightStateDto.ToggleLight)
				.Build();
			}

			var response = await _httpService.SendAsync(foundDevice.Ip.Ipv4, query);
			return new ServiceResponse<DeviceStateDto>(null, response, response ? "Light on" : "Error: Something went wrong");
		}

	}
}
