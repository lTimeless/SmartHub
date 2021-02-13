using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SmartHub.Application.UseCases.PluginAdapter.Helper;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.PluginAdapter.Creator
{
	/// <inheritdoc cref="IPluginCreatorService"/>
	public class PluginCreatorService : IPluginCreatorService
	{
		/// <inheritdoc cref="IPluginCreatorService.CreateIPluginsFromAssembly"/>
		public Dictionary<string, IPlugin> CreateIPluginsFromAssembly(Assembly assembly)
		{
			var iPluginsDictionary = new Dictionary<string, IPlugin>();
			foreach (var type in PluginHelper.GetValidPluginTypes(assembly))
			{
				var iPlugin = Activator.CreateInstance(type) as IPlugin;
				if (iPlugin != null)
				{
					object? name = iPlugin.GetType().GetProperty("Name")?.GetValue(iPlugin);
					if (name != null || name is string)
					{
						iPluginsDictionary.Add((name as string)!, iPlugin);
					}
				}
			}
			return iPluginsDictionary;
		}

		/// <inheritdoc cref="IPluginCreatorService.CreatePluginsFromIPlugins"/>
		public List<Plugin> CreatePluginsFromIPlugins(IEnumerable<IPlugin> iPluginsList, string assemblyLocation)
		{
			var pluginList = new List<Plugin>();
			var pluginsList = iPluginsList.ToList();
			if (pluginsList.IsNullOrEmpty())
			{
				return pluginList;
			}

			pluginList.AddRange(from iPlugin in pluginsList
				select new Plugin(iPlugin.Name, string.Empty, PluginHelper.GetEnumType(iPlugin.Name), assemblyLocation,
					true, iPlugin.AssemblyVersion, iPlugin.Company, PluginHelper.CombineConnectionTypes(iPlugin)));
			return pluginList;
		}
	}
}
