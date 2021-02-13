using HotChocolate;
using HotChocolate.Types;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Helper;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.BasePlugin;
using SmartHub.BasePlugin.Types;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Devices.ExtendObjectType
{
	[ExtendObjectType("Device")]
	public class ExtendDeviceType
	{
		private readonly ILogger _log = Log.ForContext(typeof(ExtendDeviceType));
		public async Task<StatusResponseType?> GetStatus([Parent]Device device, [Service] IPluginHostService pluginHostService, [Service] IHttpService httpService)
		{
			var queryTuple = new Tuple<string, Dictionary<string, string?>>("", new());
			IPlugin pluginObject;
			try
			{
				pluginObject = await pluginHostService.GetPluginByNameAsync<IPlugin>(device.PluginName);
			}
			catch (PluginException e)
			{
				_log.Information(e.Message);
				return null;
			}
			var connectionType = PluginHelper.CombineConnectionTypes(pluginObject);
			if ((connectionType & ConnectionTypes.Http) != 0 && device.PrimaryConnection == ConnectionTypes.Http)
			{
				//TODO: here get the status path/command from the actual plugin
				// queryTuple = pluginObject.InstantiateQuery().SetStatus().Build();
				queryTuple = new("/status", new());
			}
			else if ((connectionType & ConnectionTypes.Mqtt) != 0 && device.PrimaryConnection == ConnectionTypes.Mqtt)
			{
				// TODO implement later -> MQTT path
				_log.Information($"{connectionType}");
			}
			else
			{
				return null;
			}
			var (content, success) = await httpService.SendAsync<StatusResponseType>(device.Ip.Ipv4, queryTuple);
			return success? content : null;
		}
	}
}