using AutoMapper;
using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.Devices;

namespace SmartHub.Application.UseCases.Entity.Devices
{
    public class DeviceDto : BaseDto, IMapFrom<Device>
    {
        public IpAddress Ip { get; set; }

        public Company Company { get; set; }

        public ConnectionTypes PrimaryConnection { get; set; }

        public ConnectionTypes SecondaryConnection { get; set; }

        public string PluginName { get; set; } // Equals the Name Property in the IPlugin
        public PluginTypes PluginTypes { get; set; }// Equals the PluginType Property in the IPlugin

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Device, DeviceDto>()
                .ReverseMap();
        }
    }
}