using MediatR;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.PluginAdapter.Util;
using SmartHub.Domain.Enums;

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
			var foundDevice = home.Devices.SingleOrDefault(x => x.Id == request.LightStateDto.DeviceId);
			if (foundDevice is null)
			{
				return new ServiceResponse<DeviceStateDto>(false,
					$"[{nameof(DeviceLightStateHandler)}] Error: No device found by the given deviceId {request.LightStateDto.DeviceId}");
			}
			var pluginObject = await _pluginHostService.LightPlugins.GetAndLoadByName(foundDevice.PluginName, home);
			var connectionType = PluginUtils.CombineConnectionTypes(pluginObject);
			if ((connectionType & ConnectionTypeEnum.Http) != 0 && foundDevice.PrimaryConnection == ConnectionTypeEnum.Http)
			{
				_query = pluginObject.Instantiate().SetToggleLight(request.LightStateDto.ToggleLight).Build();
			}
			else if ((connectionType & ConnectionTypeEnum.Mqtt) != 0 && foundDevice.PrimaryConnection == ConnectionTypeEnum.Mqtt)
			{
				// TODO: implement later when Mqtt is useable
				Log.Information($"[{nameof(DeviceLightStateHandler)}] {connectionType}");
			}
			else
			{
				// TODO: implement later -> error path
				Log.Information($"[{nameof(DeviceLightStateHandler)}] {connectionType}");

			}
			var response = await _httpService.SendAsync(foundDevice.Ip.Ipv4, _query);
			return new ServiceResponse<DeviceStateDto>(response, response
				? $"{foundDevice.Name} changed light status"
				: $"Error: Couldn't send new light status to {foundDevice.Name}");
		}

	}
}
