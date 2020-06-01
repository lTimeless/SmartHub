using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using SmartHub.Domain.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.PluginAdapter.Finder
{
    public class PluginFinderHandler : IRequestHandler<PluginFinderQuery, ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>>
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private IPluginFinderService _pluginFinder;

        public PluginFinderHandler(ILogger logger, IUnitOfWork unitOfWork, IPluginFinderService finderService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _pluginFinder = finderService;
        }

        public async Task<ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>> Handle(PluginFinderQuery request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetFirstAsync().ConfigureAwait(false);
            var setting = home.Settings.FirstOrDefault(c => c.IsActive || c.PluginPath.Contains("_private"));
            var foundPlugins = _pluginFinder.FindPluginsInAssemblies(setting.PluginPath);

            if (!request.OnlyNew)
            {
                return new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(foundPlugins, true,
                    "New Plugins available");
            }

            var filteredOrAllFoundPlugins = await _pluginFinder.FilterByPluginsInHome(foundPlugins);
            if (filteredOrAllFoundPlugins.IsNullOrEmpty())
            {
                _logger.Warning($"[{nameof(PluginFinderHandler)}] No new Plugins available");
                return new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(filteredOrAllFoundPlugins, false, "No new Plugins available");
            }
            return new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(foundPlugins, true, "New Plugins available");
        }
    }
}
