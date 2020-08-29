using SmartHub.Application.UseCases.PluginAdapter.Loader;
using SmartHub.BasePlugin;
using SmartHub.BasePlugin.Interfaces.DeviceTypes;

namespace SmartHub.Application.UseCases.PluginAdapter.Host
{
	//TODO: der hsot wird zu der factory aus dem link. Also verwalted die dictionary aller Ts
	// bekommt dann einiger der finktionen vom Loader.... zb die welche plugins zum Home hinzufügen(werden in eine funktion verbunden "synchonize")
	// neue class PluginsManager -> welche dann alle Hosts auflisted und das Todo von weiter unten auch umsetzt
	public class PluginHostService : IPluginHostService
	{
		public IPluginLoadService<IPlugin> Plugins { get; }
		// TODO: anstatt nen loader hierfür zu haben welche eine liste von IPlugins hat (damit nicht plugins hier und in Ilight liste sind)
		// hier einfach eine funktion erstellen welche mir die listen der anderen Loader als type IPlugin kombiniert
		// IPlugin bekommt zusätzlich eine liste welche welche alle interfaces anzeigt
		// damit man schnell und einfach sieht von welchem type das plugin ist
		public IPluginLoadService<ILight> LightPlugins { get; }
		public IPluginLoadService<IDoor> DoorPlugins { get; }

		public PluginHostService(IPluginLoadService<IPlugin> pluginLoader, IPluginLoadService<ILight> lightLoader, IPluginLoadService<IDoor> doorLoader)
		{
			Plugins = pluginLoader;
			LightPlugins = lightLoader;
			DoorPlugins = doorLoader;
		}
	}
}
