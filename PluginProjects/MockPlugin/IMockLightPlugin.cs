using PluginBase;
using PluginBase.Interfaces;
using PluginBase.Interfaces.DeviceType;

namespace MockPlugin
{
    public interface IMockLightPlugin : IHttpSupport, ILight
    {

    }
}