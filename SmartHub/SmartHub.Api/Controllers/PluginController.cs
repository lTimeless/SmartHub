﻿using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Constants;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Api.Controllers
{
	public class PluginController : BaseController
	{

		/// <summary>
		/// Finds all available plugins in the plugin folder
		/// </summary>
		/// <returns>A Response with a Dictionary of "string, FoundPluginDto"</returns>
		[HttpGet(ApiRoutes.PluginRoutes.FindAll)]
		public async Task<IActionResult> FindAll()
		{
			return Ok(await Mediator.Send(new PluginFinderQuery(false)));
		}

		/// <summary>
		/// Find all new plugins in the plugin folder
		/// </summary>
		/// <returns>A Response with a Dictionary of "string, FoundPluginDto"</returns>
		[HttpGet(ApiRoutes.PluginRoutes.FindNew)]
		public async Task<IActionResult> FindNew()
		{
			return Ok(await Mediator.Send(new PluginFinderQuery(true)));
		}

		/// <summary>
		/// Loads only new Plugins from the default pluginPaths
		/// </summary>
		/// <returns>A Response containing a message</returns>
		[HttpGet(ApiRoutes.PluginRoutes.LoadOnlyNew)]
		public async Task<IActionResult> LoadOnlyNew()
		{

			return Ok(await Mediator.Send(new PluginLoadCommand(LoadStrategy.Multiple)));
		}

		/// <summary>
		/// Loads only new Plugins by the given path
		/// </summary>
		/// <returns>A Response containing a message</returns>
		[HttpGet(ApiRoutes.PluginRoutes.LoadNewByPath)]
		public async Task<IActionResult> LoadNewByPath([FromQuery]string pluginPath)
		{
			return Ok(await Mediator.Send(new PluginLoadCommand(LoadStrategy.Multiple, pluginPath)));
		}
	}
}
