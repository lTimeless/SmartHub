using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Util;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.DeviceState.LightState
{
	public class DeviceLightStateHandler : IRequestHandler<DeviceLightStateQuery, Response<DeviceStateDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPluginHostService _pluginHostService;
		private readonly IHttpService _httpService;
		private string _query = "";
		private readonly ILogger _log = Log.ForContext(typeof(DeviceLightStateHandler));
		public DeviceLightStateHandler(IUnitOfWork unitOfWork, IPluginHostService pluginHostService, IHttpService httpService)
		{
			_unitOfWork = unitOfWork;
			_pluginHostService = pluginHostService;
			_httpService = httpService;
		}

		public async Task<Response<DeviceStateDto>> Handle(DeviceLightStateQuery request, CancellationToken cancellationToken)
		{
			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}
			var home = await _unitOfWork.HomeRepository.GetHome();
			var foundDevice = home.Devices.SingleOrDefault(x => x.Id == request.LightStateDto.DeviceId);
			if (foundDevice is null)
			{
				return Response.Fail<DeviceStateDto>($"Error: No device found by the given deviceId {request.LightStateDto.DeviceId}");
			}
			var pluginObject = await _pluginHostService.LightPlugins.GetAndLoadByName(foundDevice.PluginName, home);
			var connectionType = PluginUtils.CombineConnectionTypes(pluginObject);
			if ((connectionType & ConnectionTypes.Http) != 0 && foundDevice.PrimaryConnection == ConnectionTypes.Http)
			{
				_query = pluginObject.InstantiateQuery().SetToggleLight(request.LightStateDto.ToggleLight).Build();
			}
			else if ((connectionType & ConnectionTypes.Mqtt) != 0 && foundDevice.PrimaryConnection == ConnectionTypes.Mqtt)
			{
				// TODO: implement later when Mqtt is useable
				_log.Information("{connectionType}");
			}
			else
			{
				// TODO: implement later -> error path
				_log.Information("{connectionType}");

			}
			var response = await _httpService.SendAsync(foundDevice.Ip.Ipv4, _query);
			return response ?
				Response.Ok<DeviceStateDto>($"{foundDevice.Name} changed light status", request.LightStateDto) :
				Response.Fail<DeviceStateDto>($"Error: Couldn't send new light status to {foundDevice.Name}");
		}
	}
}
