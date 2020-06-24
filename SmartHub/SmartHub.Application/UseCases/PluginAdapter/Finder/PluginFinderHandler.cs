using MediatR;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using SmartHub.Domain.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;

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
            _logger.Information($"[{nameof(PluginFinderHandler)}] Find (new = {request.OnlyNew}) available plugins");
            var home = await _unitOfWork.HomeRepository.GetHome().ConfigureAwait(false);
            var setting = home.Settings.FirstOrDefault(c => c.IsActive || c.PluginPath.Contains("_private"));
            var foundPlugins = _pluginFinder.FindPluginsInAssemblies(setting.PluginPath);

            if (!request.OnlyNew)
            {
                return foundPlugins.IsNullOrEmpty()
                    ? new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(foundPlugins, false,
                        "No plugins available")
                    : new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(foundPlugins, true,
                        "Plugins available");
            }

            var filteredOrAllFoundPlugins = await _pluginFinder.FilterByPluginsInHome(foundPlugins);
            return filteredOrAllFoundPlugins.IsNullOrEmpty()
                ? new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(filteredOrAllFoundPlugins,
                    false,
                    "No new plugins available")
                : new ServiceResponse<IReadOnlyDictionary<string, FoundPluginDto>>(foundPlugins,
                    true,
                    "New plugins available");
        }
    }
}
