using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.BasePlugin;

namespace SmartHub.Api.Controllers
{
	public class PluginController : BaseController
	{
		private readonly IPluginHostService _pluginHostService;

		public PluginController(IPluginHostService pluginHostService)
		{
			_pluginHostService = pluginHostService;
		}

		/// <summary>
		/// Loads only new Plugins by the given path
		/// </summary>
		/// <returns>A Response containing a message</returns>
		[AllowAnonymous]
		[HttpGet("getByName")]
		[Obsolete("This is just for testing purposes")]
		public async Task<IActionResult> GetByName([FromQuery]string pluginName)
		{
			return Ok(await _pluginHostService.GetPluginByNameAsync<IPlugin>(pluginName));
		}

		/// <summary>
		/// Finds all available plugins in the plugin folder (on hdd not db)
		/// </summary>
		/// <returns>A Response with a Dictionary of "string, FoundPluginDto"</returns>
		[HttpGet("findAll")]
		[Obsolete("This will be later implemented with the downloadFromServer function")]
		public IActionResult FindAll()
		{
			return Ok("This is not implemented");
		}

		/// <summary>
		/// Find all new plugins in the plugin folder (on hdd not db)
		/// </summary>
		/// <returns>A Response with a Dictionary of "string, FoundPluginDto"</returns>
		[HttpGet("findNew")]
		[Obsolete("This will be later implemented with the downloadFromServer function")]
		public IActionResult FindNew()
		{
			return Ok("This is not implemented");
		}

		/// <summary>
		/// Loads only new Plugins from the plugin folder
		/// </summary>
		/// <returns>A Response containing a message</returns>
		[HttpGet("loadAllNew")]
		[Obsolete("This will be later implemented with the downloadFromServer function")]
		public IActionResult LoadAllNew()
		{
			return Ok("This is not implemented");
		}

		/// <summary>
		/// Loads only new Plugins by the given path
		/// </summary>
		/// <returns>A Response containing a message</returns>
		[HttpGet("loadNewBypath")]
		[Obsolete("This will be later implemented with the downloadFromServer function")]
		public IActionResult LoadNewByPath([FromQuery]string pluginPath)
		{
			return Ok("This is not implemented");
		}
	}
}
