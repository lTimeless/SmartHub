using System.Runtime.Loader;

namespace SmartHub.Application.UseCases.PluginAdapter
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        public PluginLoadContext() : base($"{nameof(PluginLoadContext)}", true)
        {
        }
    }
}
