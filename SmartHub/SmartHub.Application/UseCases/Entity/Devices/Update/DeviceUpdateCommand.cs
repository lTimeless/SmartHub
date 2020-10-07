using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Entity.Devices.Update
{
    public class DeviceUpdateCommand : IRequest<Response<DeviceDto>>
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Ipv4 { get; set; }
        public ConnectionTypes PrimaryConnection { get; set; }
        public ConnectionTypes SecondaryConnection { get; set; }

        public DeviceUpdateCommand(ConnectionTypes secondaryConnection, ConnectionTypes primaryConnection, string? ipv4, string? description, string? name, string id)
        {
            SecondaryConnection = secondaryConnection;
            PrimaryConnection = primaryConnection;
            Ipv4 = ipv4;
            Description = description;
            Name = name;
            Id = id;
        }
    }
}