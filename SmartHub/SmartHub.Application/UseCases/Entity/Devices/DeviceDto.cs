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

        public string PrimaryConnection { get; set; }

        public string SecondaryConnection { get; set; }

        public string PluginName { get; set; } // Equals the Name Property in the IPlugin
        public string PluginTypes { get; set; }// Equals the PluginType Property in the IPlugin

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Device, DeviceDto>()
                .ForMember(x => x.PluginTypes,
                    x =>
                        x.MapFrom(d => d.PluginTypes.ToString()))
                .ForMember(x => x.PrimaryConnection, x =>
                    x.MapFrom(d => d.PrimaryConnection.ToString()))
                .ForMember(x => x.SecondaryConnection, x =>
                    x.MapFrom(d => d.SecondaryConnection.ToString()));
        }
    }
}