using MediatR;
using SmartHub.Domain.Common;
using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    public class PluginLoadCommand : IRequest<ServiceResponse<string>>
    {
        public string Path { get; }
        public LoadStrategyEnum LoadStrategyMultiple { get;  }


        public PluginLoadCommand(LoadStrategyEnum loadStrategyMultiple, string? path)
        {
            LoadStrategyMultiple = loadStrategyMultiple;
            Path = path ?? string.Empty;
        }
    }
}
