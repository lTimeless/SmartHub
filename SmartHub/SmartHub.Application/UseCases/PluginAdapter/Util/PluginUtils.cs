using SmartHub.BasePlugin;
using SmartHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

		public static PluginTypeEnum GetEnumType(string pluginName)
		{
			return pluginName switch
			{
				nameof(PluginTypeEnum.Base) => PluginTypeEnum.Base,
				nameof(PluginTypeEnum.Mock) => PluginTypeEnum.Mock,
				nameof(PluginTypeEnum.Door) => PluginTypeEnum.Door,
				nameof(PluginTypeEnum.Light) => PluginTypeEnum.Light,
				nameof(PluginTypeEnum.Ht) => PluginTypeEnum.Ht,
				nameof(PluginTypeEnum.Sensor) => PluginTypeEnum.Sensor,
				nameof(PluginTypeEnum.Rgb) => PluginTypeEnum.Rgb,
				_ => PluginTypeEnum.None,
			};
		}
	}
}
