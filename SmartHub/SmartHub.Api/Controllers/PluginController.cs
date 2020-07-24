using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Api.Controllers
{
	public class PluginController : BaseController
	{

		/// <summary>
		/// Finds all available plugins in the plugin folder
		/// </summary>
		/// <returns></returns>
		[HttpGet("find")]
		public async Task<IActionResult> FindAll()
		{
			return Ok(await Mediator.Send(new PluginFinderQuery(false)));
		}

		/// <summary>
		/// Find all new plugins in the plugin folder
		/// </summary>
		/// <returns></returns>
		[HttpGet("findNew")]
		public async Task<IActionResult> FindNew()
		{
			return Ok(await Mediator.Send(new PluginFinderQuery(true)));
		}

		// GET: api/Plugin
		[HttpGet("loadAllNew")]
		public async Task<IActionResult> LoadAllNew()
		{

			return Ok(await Mediator.Send(new PluginLoadCommand(LoadStrategy.Multiple)));
		}

		// GET: api/Plugin/
		[HttpGet("{pluginPath}", Name = "Get")]
		public async Task<IActionResult> LoadNewBypath(string pluginPath)
		{
			return Ok(await Mediator.Send(new PluginLoadCommand(LoadStrategy.Multiple, pluginPath)));
		}
	}
}
