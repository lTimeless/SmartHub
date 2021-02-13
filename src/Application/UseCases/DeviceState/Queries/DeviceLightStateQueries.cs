using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;
using SmartHub.Application.UseCases.PluginAdapter.Helper;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.BasePlugin.Interfaces.DeviceTypes;
using SmartHub.BasePlugin.Types;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.DeviceState.Queries
{
	/// <summary>
	/// All queries for setting the DeviceLightState.
	/// </summary>
	[Authorize]
	[GraphQLDescription("All queries for setting the DeviceLightState.")]
	public class DeviceLightStateQueries
	{
		private readonly ILogger _log = Log.ForContext(typeof(DeviceLightStateQueries));

		/// <summary>
		///
		/// </summary>
		/// <param name="appConfigService"></param>
		/// <param name="pluginHostService"></param>
		/// <param name="httpService"></param>
		/// <param name="deviceRepository"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task<DeviceStatePayload> SetLightState([Service] IAppConfigService appConfigService,
			[Service] IPluginHostService pluginHostService, [Service] IHttpService httpService,
			[Service] IBaseRepositoryAsync<Device> deviceRepository, DeviceLightStateInput input)
		{
			if (string.IsNullOrWhiteSpace(input.DeviceId))
			{
				return new (new("Error: No deviceId specified.", AppErrorCodes.IsEmpty));
			}
			
			var appConfig = appConfigService.GetConfig();
			if (appConfig.IsActive is false)
			{
				return new(new("Error: There is no home created at the moment.", AppErrorCodes.NoHome));
			}

			var foundDevice = await deviceRepository.FindByAsync(x => x.Id == input.DeviceId);
			if (foundDevice == null)
			{
				return new(new($"Error:  No device found for {input.DeviceId}.",AppErrorCodes.NotFound));
			}
			var queryTuple = new Tuple<string, Dictionary<string, string?>>("", new());
			ILight pluginObject;
			try
			{
				pluginObject = await pluginHostService.GetPluginByNameAsync<ILight>(foundDevice.PluginName);
			}
			catch (PluginException e)
			{
				return new(new($"Error:  {e.Message}.",AppErrorCodes.NotFound));
			}
			var connectionType = PluginHelper.CombineConnectionTypes(pluginObject);
			if ((connectionType & ConnectionTypes.Http) != 0 && foundDevice.PrimaryConnection == ConnectionTypes.Http)
			{
				queryTuple = pluginObject.InstantiateQuery().SetLight(input.SetLight).Build();
			}
			else if ((connectionType & ConnectionTypes.Mqtt) != 0 && foundDevice.PrimaryConnection == ConnectionTypes.Mqtt)
			{
				// TODO implement later -> MQTT path
				_log.Information($"{connectionType}");
			}
			else
			{
				return new(new($"Error:  No connection type set for device {foundDevice.Name}.",AppErrorCodes.NotSet));
			}
			var (content, success) = await httpService.SendAsync<LightResponseType>(foundDevice.Ip.Ipv4, queryTuple);
			return success
				? new(content, $"{foundDevice.Name} changed light status")
				: new(new("Error: Could not set device light state.", AppErrorCodes.NotSet));
		}
	}
}