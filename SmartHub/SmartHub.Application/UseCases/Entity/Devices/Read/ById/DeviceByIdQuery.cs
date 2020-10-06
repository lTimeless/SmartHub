using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Devices.Read.ById
{
    public class DeviceByIdQuery : IRequest<Response<DeviceDto>>
    {
        public string Id { get; set; }

        public DeviceByIdQuery(string id)
        {
            Id = id;
        }
    }
}