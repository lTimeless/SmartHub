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
    public class PluginFinderHandler : IRequestHandler<PluginFinderQuery, Response<IReadOnlyDictionary<string, FoundPluginDto>>>
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

        public async Task<Response<IReadOnlyDictionary<string, FoundPluginDto>>> Handle(PluginFinderQuery request, CancellationToken cancellationToken)
        {
            _logger.Information($"Find (new = {request.OnlyNew}) available plugins");
            var home = await _unitOfWork.HomeRepository.GetHome().ConfigureAwait(false);
            if (home == null)
            {
                return Response.Fail<IReadOnlyDictionary<string, FoundPluginDto>>("No home available.");
            }
            var setting = home.Settings.FirstOrDefault(c => c.IsActive || c.PluginPath.Contains("_private"));
            var foundPlugins = _pluginFinder.FindPluginsInAssemblies(setting.PluginPath);

            if (!request.OnlyNew)
            {
                return foundPlugins.IsNullOrEmpty()
                    ? Response.Fail<IReadOnlyDictionary<string, FoundPluginDto>>("No plugins available")
                    : Response.Ok("Plugins available", foundPlugins);
            }

            var filteredOrAllFoundPlugins = await _pluginFinder.FilterByPluginsInHome(foundPlugins);
            return filteredOrAllFoundPlugins.IsNullOrEmpty()
                ? Response.Fail<IReadOnlyDictionary<string, FoundPluginDto>>("No new plugins available")
                : Response.Ok("New plugins available", filteredOrAllFoundPlugins);
        }
    }
}
