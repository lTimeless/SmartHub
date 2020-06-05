using SmartHub.Application.UseCases.PluginAdapter.Util;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Extensions;
using SmartHub.Domain.Entities.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartHub.Application.UseCases.PluginAdapter.Creator
{
	public class PluginCreatorService<T> : IPluginCreatorService<T> where T : class
	{
		public PluginCreatorService()
		{
		}

		public Dictionary<string, T> CreateIPluginsFromAssembly(Assembly assembly)
		{
			var iPluginsDictionary = new Dictionary<string, T>();
			foreach (var type in PluginUtils.GetPluginTypes(assembly))
			{
				var plugin = Activator.CreateInstance(type) as T;
				switch (plugin)
				{
					case null:
						continue;
					case IPlugin iPlugin:
						iPluginsDictionary.Add(iPlugin.Name, plugin);
						break;
				}
			}
			return iPluginsDictionary;
		}

		public List<Plugin> CreatePluginsFromIPlugins(IEnumerable<IPlugin> iPluginsList, string assemblyLocation)
		{
			var pluginList = new List<Plugin>();
			if (iPluginsList.IsNullOrEmpty())
			{
				return pluginList;
			}

			// TODO: den support anhand des interfaces herausfinden, erbt IPlugin von dem jeweiligeninterface dann setze auf true
			pluginList.AddRange(iPluginsList
				.Select(iPlugin => new Plugin(iPlugin.Name, string.Empty, PluginUtils.GetEnumType(iPlugin.Name), assemblyLocation, true,
					iPlugin.AssemblyVersion, iPlugin.Company, iPlugin.MqttSupport, iPlugin.HttpSupport)));
			return pluginList;
		}
	}
}
