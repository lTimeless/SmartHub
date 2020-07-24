using SmartHub.BasePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Util
{
	public static class PluginUtils
	{
		public static IEnumerable<Type> GetValidPluginTypes(Assembly assembly)
		{
			return assembly.GetTypes()
				.Where(type => !type.IsInterface && typeof(IPlugin).IsAssignableFrom(type))
				.ToList();
		}

		public static PluginTypes GetEnumType(string pluginName)
		{
			return pluginName switch
			{
				nameof(PluginTypes.Base) => PluginTypes.Base,
				nameof(PluginTypes.Mock) => PluginTypes.Mock,
				nameof(PluginTypes.Door) => PluginTypes.Door,
				nameof(PluginTypes.Light) => PluginTypes.Light,
				nameof(PluginTypes.Ht) => PluginTypes.Ht,
				nameof(PluginTypes.Sensor) => PluginTypes.Sensor,
				nameof(PluginTypes.Rgb) => PluginTypes.Rgb,
				_ => PluginTypes.None
			};
		}

		public static ConnectionTypes CombineConnectionTypes(IPlugin iPlugin)
		{
			var interfaces = iPlugin.GetType().GetInterfaces();
			var connectionTyp = ConnectionTypes.None;
			var httpSupport = interfaces.Any(x => x.Name.Contains("HttpSupport"));
			var mqttSupport = interfaces.Any(x => x.Name.Contains("MqttSupport"));

			if (httpSupport)
			{
				connectionTyp |= ConnectionTypes.Http;
			}
			if (mqttSupport)
			{
				connectionTyp |= ConnectionTypes.Mqtt;
			}

			return connectionTyp;
		}
	}
}
