using System.Collections.Generic;
using SmartHub.Application.Common.Mappings;
using SmartHub.Application.UseCases.Entity.Settings;
using SmartHub.Application.UseCases.Entity.Users;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Homes
{
    public class HomeDto : BaseDto, IMapFrom<Home>
    {
        public List<SettingDto> Settings { get; set; }
        public List<UserDto> Users { get; set; }
    }
}