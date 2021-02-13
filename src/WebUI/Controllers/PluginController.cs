using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.BasePlugin;
using System;
using System.Threading.Tasks;

namespace SmartHub.WebUI.Controllers
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
	}
}
