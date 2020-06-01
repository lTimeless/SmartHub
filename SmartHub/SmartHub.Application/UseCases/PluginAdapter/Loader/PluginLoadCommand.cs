using MediatR;
using SmartHub.Domain.Common;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    public class PluginLoadCommand : IRequest<ServiceResponse<string>>
    {
        public bool LoadOne { get; }
        public string? Path { get; }


        public PluginLoadCommand(bool loadOne, string path = null)
        {
            LoadOne = loadOne;
            Path = path;
        }
    }
}
