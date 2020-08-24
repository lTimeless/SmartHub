using MediatR;
using Serilog;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Domain.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    public class PluginLoadHandler : IRequestHandler<PluginLoadCommand, Response<string>>
    {
        private readonly IPluginHostService _pluginHostService;
        private readonly ILogger _log = Log.ForContext(typeof(PluginLoadHandler));
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPluginFinderService _pluginFinderService;

        public PluginLoadHandler(IUnitOfWork unitOfWork, IPluginFinderService pluginFinderService, IPluginHostService pluginHostService)
        {
            _unitOfWork = unitOfWork;
            _pluginFinderService = pluginFinderService;
            _pluginHostService = pluginHostService;
        }

        public async Task<Response<string>> Handle(PluginLoadCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home is null)
            {
                _log.Warning($"[{nameof(PluginLoadHandler)}] No home available.");
                return Response.Fail<string>("No home available.");
            }
            var setting = home.Settings.FirstOrDefault(c => c.IsActive || c.PluginPath.Contains("_private"));

            var foundPlugins = _pluginFinderService.FindPluginsInAssemblies(setting.PluginPath);
            var filteredOrAllFoundPlugins = await _pluginFinderService.FilterByPluginsInHome(foundPlugins);

            if (filteredOrAllFoundPlugins.IsNullOrEmpty())
            {
                _log.Warning($"[{nameof(PluginLoadHandler)}] No new plugins available.");
                return Response.Fail<string>("No new plugins available.");
            }
            // TODO: why is this null
            var path = request.Path.IsNullOrEmpty() ? setting.PluginPath : request.Path;

            var pluginsLoaded = await _pluginHostService.Plugins.LoadAndAddToHomeAsync(new []{ path }, request.LoadStrategyMultiple);
            return pluginsLoaded
                ? Response.Ok("New Plugins loaded.")
                : Response.Fail<string>("Error: Couldn't load new Plugins.");
        }
    }
}
