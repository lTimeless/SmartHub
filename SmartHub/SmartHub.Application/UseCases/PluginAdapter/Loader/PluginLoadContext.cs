using System.Runtime.Loader;

namespace SmartHub.Application.UseCases.PluginAdapter.Loader
{
    /// <summary>
    /// The AssemblyLoadContext for plugins
    /// </summary>
    public class PluginLoadContext : AssemblyLoadContext
    {
        public PluginLoadContext() : base($"{nameof(PluginLoadContext)}", true)
        {
        }
    }
}
