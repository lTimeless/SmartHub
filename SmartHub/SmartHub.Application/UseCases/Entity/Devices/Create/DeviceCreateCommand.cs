using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Entity.Devices.Create
{
    public class DeviceCreateCommand : IRequest<Response<string>>
    {
        public string Name { get; }
        public string? Description { get; }
        public string Ipv4 { get; }
        public string CompanyName { get; }
        public string PluginName { get;} // Equals the Name Property in the IPlugin
        public PluginTypes PluginTypes { get; }// Equals the PluginType Property in the IPlugin
        public ConnectionTypes PrimaryConnection { get; }

        public ConnectionTypes SecondaryConnection { get; }
        public string GroupName { get; set; }

        public DeviceCreateCommand(string name,
            string? description,
            string ipv4,
            string pluginName,
            PluginTypes pluginTypes,
            ConnectionTypes primaryConnection,
            ConnectionTypes secondaryConnection,
            string groupName, string companyName)
        {
            Name = name;
            Description = description;
            Ipv4 = ipv4;
            PluginName = pluginName;
            PluginTypes = pluginTypes;
            PrimaryConnection = primaryConnection;
            SecondaryConnection = secondaryConnection;
            GroupName = groupName;
            CompanyName = companyName;
        }
    }
}