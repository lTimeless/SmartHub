using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SmartHub.BasePlugin;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Helper
{
	public static class PluginHelper
	{
		/// <summary>
		/// gets valid pluginTypes
		/// </summary>
		/// <param name="assembly">The assembly to check</param>
		/// <returns>An IEnumerable with valid Types</returns>
		public static IEnumerable<Type> GetValidPluginTypes(Assembly assembly)
		{
			return assembly.GetTypes()
				.Where(type => !type.IsInterface && !type.IsAbstract && typeof(IPlugin).IsAssignableFrom(type))
				.ToList();
		}

		/// <summary>
		/// Gets the enum to the given string
		/// </summary>
		/// <param name="pluginName">The name you want to have as an enum</param>
		/// <returns>The found pluginType, or PluginTypes.None if nothing matches</returns>
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

		/// <summary>
		/// Gets the interfaces from the plugin and combines all connectionTypes inside an enum
		/// </summary>
		/// <param name="iPlugin">The plugin to check the interfaces</param>
		/// <returns>The found and combined connectionTypes</returns>
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

		/// <summary>
		/// Validates the given path if it is a Dictionary or a .dll File
		/// </summary>
		/// <param name="path">The path to look for</param>
		/// <returns>false if path does not exist</returns>
		public static bool ValidatePath(string path)
		{
			var pathInfo = File.GetAttributes(path);
			if ((pathInfo & FileAttributes.Directory) == FileAttributes.Directory)
			{
				if (!Directory.Exists(path))
				{
					return false;
				}
			}
			if ((pathInfo & FileAttributes.Archive) == FileAttributes.Archive)
			{
				if (!File.Exists(path))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Filters the given dictionary with the given function
		/// </summary>
		/// <param name="foundPluginDtosDictionary">all previously found PluginDtos</param>
		/// <param name="function">Function that checks if pluginExists</param>
		/// <returns></returns>
		public static IReadOnlyDictionary<string, PluginDto> FilterByFunction(
			IReadOnlyDictionary<string, PluginDto> foundPluginDtosDictionary, Func<string, bool> function)
		{
			var finalDictionary = new Dictionary<string, PluginDto>();
			foreach (var (key, value) in foundPluginDtosDictionary)
			{
				if(function.Invoke(key))
				{
					continue;
				}
				finalDictionary.Add(key, value);
			}
			return finalDictionary;
		}
	}
}
