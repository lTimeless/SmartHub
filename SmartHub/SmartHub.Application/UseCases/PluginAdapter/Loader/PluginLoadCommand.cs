using MediatR;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    public class PluginLoadCommand : IRequest<ServiceResponse<string>>
    {
        public string Path { get; }


        public PluginLoadCommand(string path = null)
        {
            Path = path is null ? string.Empty : path;
        }
    }
}
