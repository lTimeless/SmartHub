using Microsoft.AspNetCore.Mvc;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Application.UseCases.PluginAdapter.Loader;
using System;
using System.Threading.Tasks;

namespace SmartHub.Api.Controllers
{
	public class PluginController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;
		private readonly IPluginHostService _pluginHostService;

		public PluginController(IUnitOfWork unitOfWork, ILogger logger, IPluginHostService pluginHostService)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_pluginHostService = pluginHostService;
		}

		/// <summary>
		/// Finds all available plugins in the plugin folder
		/// </summary>
		/// <returns></returns>
		[HttpGet("find")]
		public async Task<IActionResult> Find()
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

			return Ok(await Mediator.Send(new PluginLoadCommand()));
		}

		// GET: api/Plugin/
		[HttpGet("{pluginPath}", Name = "Get")]
		public async Task<IActionResult> LoadNewBypath(string pluginPath)
		{
			return Ok(await Mediator.Send(new PluginLoadCommand(pluginPath)));
		}


		// GET: api/Plugin/plugins
		[HttpGet("plugins")]
		[Obsolete("Just for testing")]
		public async Task<IActionResult> Plugins()
		{
			var home = await _unitOfWork.HomeRepository.GetFirstAsync();
			var plugins = home.Plugins;
			return Ok(plugins);
		}

		// GET: api/Plugin/Iplugins
		[HttpGet("iplugins/{pluginName}")]
		public async Task<IActionResult> IPlugins(string pluginName)
		{
			var iPlugins = await _pluginHostService.Plugins.GetAndLoadByName(pluginName, null);
			return Ok(iPlugins);
		}

	}
}
