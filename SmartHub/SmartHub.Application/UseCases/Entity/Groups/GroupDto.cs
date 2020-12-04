using System.Collections.Generic;
using SmartHub.Application.Common.Mappings;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
    public class GroupDto : BaseDto, IMapFrom<Group>
    {
	    public bool IsSubGroup { get; set; }
        public List<DeviceDto>? Devices { get; set; }
        public List<GroupDto>? SubGroups { get; set; }
    }
}