using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Enums;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    public class PluginLoadCommand : IRequest<Response<string>>
    {
        public string Path { get; set; }
        public LoadStrategy LoadStrategyMultiple { get; set; }


        public PluginLoadCommand(LoadStrategy loadStrategyMultiple) : this(loadStrategyMultiple, string.Empty)
        {
        }

        public PluginLoadCommand(LoadStrategy loadStrategyMultiple, string path)
        {
            LoadStrategyMultiple = loadStrategyMultiple;
            Path = path;
        }
    }
}
