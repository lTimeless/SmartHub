using System;

namespace SmartHub.BasePlugin
{
    public interface IPlugin
    {
        string Id { get; set; }
        public string Name { get; set; }
        string Company { get; set; }
        DateTime ModifiedAt { get; }
        DateTime CreatedAt { get; }
        //TODO: vlt ersetzen durch interfaces
        public bool MqttSupport { get; set; }
        public bool HttpSupport { get; set; }
        /// <summary>
        /// Build version of the Assembly
        /// </summary>
        double AssemblyVersion { get; }
    }
}