using System;
using System.Linq;
using System.Text;
using PluginBase;
using PluginBase.Interfaces;
using PluginBase.Interfaces.DeviceType;

namespace MockPlugin
{
    public class MockLightPlugin : IMockLightPlugin
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = $"{nameof(MockLightPlugin)}";
        public string Company { get; set; } = $"{typeof(MockLightPlugin).Assembly.FullName.Split("P")[0]}";
        public DateTime ModifiedAt { get; }
        public DateTime CreatedAt { get; }
        public bool MqttSupport { get; set; } = false;
        public bool HttpSupport { get; set; } = true;
        public double AssemblyVersion { get; }
        public string TurnOnOff { get; set; } = "turn=";
        private StringBuilder Builder { get; set; }

        ILight ILight.SetToggleLight(bool? onOff)
        {
            if (onOff is null)
            {
                return this;
            }
            if (onOff.Value)
            {
                Builder.Append(TurnOnOff + "On");
            }
            if(onOff.Value == false)
            {
                Builder.Append(TurnOnOff + "Off");
            }
            return this;
        }

        public ILight SetRgba(int red, int green, int blue, int alpha)
        {
            //TODO: Implement logic
            return this;
        }

        ILight IBuild<ILight>.Instantiate()
        {
            Builder = new StringBuilder();
            Builder.Append("?");
            return this;
        }

        public string Build()
        {
            return Builder.ToString();
        }
    }
}