using MediatR;
using SmartHub.Domain.Common;
using System.Collections.Generic;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.PluginAdapter.Finder
{
    public class PluginFinderQuery : IRequest<Response<IReadOnlyDictionary<string, FoundPluginDto>>>
    {
        public bool OnlyNew { get; }

        public PluginFinderQuery(bool onlyNew)
        {
            OnlyNew = onlyNew;
        }
    }
}
