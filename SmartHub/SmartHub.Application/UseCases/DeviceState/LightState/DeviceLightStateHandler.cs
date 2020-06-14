using MediatR;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateHandler : IRequestHandler<DeviceLightStateQuery, ServiceResponse<DeviceStateDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginHostService _pluginHostService;
		private readonly IHttpService _httpService;
		private string _query = "";

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
			var home = await _unitOfWork.HomeRepository.GetHome();
			var foundDevice = home.Devices.Find(x => x.Id == request.LightStateDto.DeviceId);
			if (foundDevice is null)
			{
				return new ServiceResponse<DeviceStateDto>(false,
					$"[{nameof(DeviceLightStateHandler)}] Error: No device found by the given deviceId {request.LightStateDto.DeviceId}");
			}
			var pluginObject = await _pluginHostService.LightPlugins.GetAndLoadByName(foundDevice.PluginName, home);
			if (pluginObject.HttpSupport)
			{
				_query = pluginObject.Instantiate()
					.SetToggleLight(request.LightStateDto.ToggleLight)
				.Build();
			}

			var response = await _httpService.SendAsync(foundDevice.Ip.Ipv4, _query);
			return new ServiceResponse<DeviceStateDto>(response, response
				? $"{foundDevice.Name} changed light status"
				: "Error: Something went wrong");
		}

	}
}
