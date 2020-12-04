using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.PluginAdapter.Helper;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.BasePlugin.Interfaces.DeviceTypes;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateHandler : IRequestHandler<DeviceLightStateQuery, Response<DeviceStateDto>>
	{
		private readonly IPluginHostService _pluginHostService;
		private readonly IHttpService _httpService;
		private readonly IAppConfigService _appConfigService;
		private readonly IBaseRepositoryAsync<Device> _deviceRepository;

		private string _query = "";
		private readonly ILogger _log = Log.ForContext(typeof(DeviceLightStateHandler));
		public DeviceLightStateHandler(IPluginHostService pluginHostService, IHttpService httpService, IAppConfigService appConfigService, IBaseRepositoryAsync<Device> deviceRepository)
		{
			_pluginHostService = pluginHostService;
			_httpService = httpService;
			_appConfigService = appConfigService;
			_deviceRepository = deviceRepository;
		}

		public async Task<Response<DeviceStateDto>> Handle(DeviceLightStateQuery request, CancellationToken cancellationToken)
		{
			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}
			var appConfig = _appConfigService.GetConfig();
			if (appConfig.IsActive is false)
			{
				return Response.Fail<DeviceStateDto>("Error: There is no home created at the moment.", new DeviceLightStateRequestDto());

			}

			var foundDevice = await _deviceRepository.FindByAsync(x => x.Id == request.LightStateDto.DeviceId);
			if (foundDevice is null)
			{
				return Response.Fail<DeviceStateDto>($"Error: No device found by the given deviceId {request.LightStateDto.DeviceId}", new DeviceLightStateRequestDto());
			}
			var pluginObject = await _pluginHostService.GetPluginByNameAsync<ILight>(foundDevice.PluginName);
			var connectionType = PluginHelper.CombineConnectionTypes(pluginObject);
			if ((connectionType & ConnectionTypes.Http) != 0 && foundDevice.PrimaryConnection == ConnectionTypes.Http)
			{
				_query = pluginObject.InstantiateQuery().SetToggleLight(request.LightStateDto.ToggleLight).Build();
			}
			else if ((connectionType & ConnectionTypes.Mqtt) != 0 && foundDevice.PrimaryConnection == ConnectionTypes.Mqtt)
			{
				// TODO implement later when Mqtt is useable
				_log.Information("{connectionType}");
			}
			else
			{
				// TODO implement later -> error path
				_log.Information("{connectionType}");

			}
			// var response = await _httpService.SendAsync(foundDevice.Ip.Ipv4, _query);
			return true ?
				Response.Ok<DeviceStateDto>($"{foundDevice.Name} changed light status", request.LightStateDto) :
				Response.Fail<DeviceStateDto>($"Error: Couldn't send new light status to {foundDevice.Name}", new DeviceLightStateRequestDto());
		}
	}
}
