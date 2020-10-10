using System.Collections.Generic;
using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Devices.Read
{
    public class DeviceGetQuery : IRequest<Response<IEnumerable<DeviceDto>>>
    {

    }
}