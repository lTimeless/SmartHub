﻿using System.Collections.Generic;
using SmartHub.Application.Common.Mappings;
using SmartHub.Application.UseCases.Entity.Configurations;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Application.UseCases.Entity.Users;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Homes
{
    public class HomeDto : BaseDto, IMapFrom<Home>
    {
        public List<ConfigurationDto>? Configurations { get; set; }
        public List<UserDto>? Users { get; set; }
        public List<GroupDto>? Groups { get; set; }
    }
}