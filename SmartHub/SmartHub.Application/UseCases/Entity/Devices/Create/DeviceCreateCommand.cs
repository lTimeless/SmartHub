using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Devices.Create
{
    public class DeviceCreateCommand : IRequest<Response<DeviceDto>>{
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Ipv4 { get; set; }
        public string Manufacturer { get; set; }
        public string PluginName { get; set; } // Equals the Name Property in the IPlugin
        public string PluginTypes { get; set; }// Equals the PluginType Property in the IPlugin
        public string PrimaryConnection { get; set; }

        public string? SecondaryConnection { get; set; }
        public string GroupId { get; set; }

        public DeviceCreateCommand(string name,
            string? description,
            string ipv4,
            string pluginName,
            string pluginTypes,
            string primaryConnection,
            string? secondaryConnection,
            string groupId, string manufacturer)
        {
            Name = name;
            Description = description;
            Ipv4 = ipv4;
            PluginName = pluginName;
            PluginTypes = pluginTypes;
            PrimaryConnection = primaryConnection;
            SecondaryConnection = secondaryConnection;
            GroupId = groupId;
            Manufacturer = manufacturer;
        }
    }
}