using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities.Devices;

namespace SmartHub.Application.UseCases.Entity.Devices.Create
{
    public class DeviceCreateCommand : IRequest<Response<DeviceDto>>{
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Ipv4 { get; set; }
        public string CompanyName { get; set; }
        public string PluginName { get; set; } // Equals the Name Property in the IPlugin
        public PluginTypes PluginTypes { get; set; }// Equals the PluginType Property in the IPlugin
        public ConnectionTypes PrimaryConnection { get; set; }

        public ConnectionTypes? SecondaryConnection { get; set; }
        public string GroupId { get; set; }

        public DeviceCreateCommand(string name,
            string? description,
            string ipv4,
            string pluginName,
            PluginTypes pluginTypes,
            ConnectionTypes primaryConnection,
            ConnectionTypes? secondaryConnection,
            string groupId, string companyName)
        {
            Name = name;
            Description = description;
            Ipv4 = ipv4;
            PluginName = pluginName;
            PluginTypes = pluginTypes;
            PrimaryConnection = primaryConnection;
            SecondaryConnection = secondaryConnection;
            GroupId = groupId;
            CompanyName = companyName;
        }
    }
}