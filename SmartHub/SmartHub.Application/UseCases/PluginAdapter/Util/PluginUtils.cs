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
		public static List<Type> GetPluginTypes(Assembly assembly)
		{
			return assembly.GetTypes().Where(type => !type.IsInterface)
				.Where(type => typeof(IPlugin).IsAssignableFrom(type))
				.ToList();

		}

		// TODO: refactor wie in dev.to article?? Scheint auch nicht richtig zu funktionieren
		public static PluginTypeEnum GetEnumType(string pluginName)
		{
			if (Enum.TryParse<PluginTypeEnum>(pluginName, out var tryTypeResult))
			{
				return tryTypeResult switch
				{
					PluginTypeEnum.Base => PluginTypeEnum.Base,
					PluginTypeEnum.Mock => PluginTypeEnum.Mock,
					PluginTypeEnum.Door => PluginTypeEnum.Door,
					PluginTypeEnum.Light => PluginTypeEnum.Light,
					PluginTypeEnum.Ht => PluginTypeEnum.Ht,
					PluginTypeEnum.Sensor => PluginTypeEnum.Sensor,
					PluginTypeEnum.Rgb => PluginTypeEnum.Rgb,
					_ => PluginTypeEnum.None,
				};
			}
			return PluginTypeEnum.Base;
		}
	}
}
