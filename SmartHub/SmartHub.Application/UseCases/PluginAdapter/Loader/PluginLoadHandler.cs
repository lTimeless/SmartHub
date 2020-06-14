using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.UseCases.PluginAdapter.Finder;
using SmartHub.Application.UseCases.PluginAdapter.Host;
using SmartHub.Domain.Common;
using SmartHub.Domain.Common.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    public class PluginLoadHandler : IRequestHandler<PluginLoadCommand, ServiceResponse<string>>
    {
        private readonly IPluginHostService _pluginHostService;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPluginFinderService _pluginFinderService;

        public PluginLoadHandler(ILogger logger, IUnitOfWork unitOfWork, IPluginFinderService pluginFinderService, IPluginHostService pluginHostService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _pluginFinderService = pluginFinderService;
            _pluginHostService = pluginHostService;
        }

        public async Task<ServiceResponse<string>> Handle(PluginLoadCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetFirstAsync().ConfigureAwait(false);
            var setting = home.Settings.FirstOrDefault(c => c.IsActive || c.PluginPath.Contains("_private"));

            var foundPlugins = _pluginFinderService.FindPluginsInAssemblies(setting.PluginPath);
            var filteredOrAllFoundPlugins = await _pluginFinderService.FilterByPluginsInHome(foundPlugins);

            if (filteredOrAllFoundPlugins.IsNullOrEmpty())
            {
                _logger.Warning($"[{nameof(PluginLoadHandler)}] No new plugins available");
                return new ServiceResponse<string>(null, false,
                    "No new plugins available");
            }
            var path = request.Path.IsNullOrEmpty() ? setting.PluginPath : request.Path;

            var pluginsLoaded = await _pluginHostService.Plugins.LoadAndAddToHomeAsync(new []{ path }, request.LoadStrategyMultiple);
            return new ServiceResponse<string>(pluginsLoaded ? "New Plugins loaded" : "Error loading new Plugins",
                pluginsLoaded);
        }
    }
}
