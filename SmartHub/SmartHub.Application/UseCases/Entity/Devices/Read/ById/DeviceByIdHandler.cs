using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices.Read.ById
{
	public class DeviceByIdHandler : IRequestHandler<DeviceByIdQuery, Response<DeviceDto>>
    {
        private readonly IMapper _mapper;
		private readonly IBaseRepositoryAsync<Device> _deviceRepositry;

		public DeviceByIdHandler(IMapper mapper, IBaseRepositoryAsync<Device> deviceRepositry)
		{
			_mapper = mapper;
			_deviceRepositry = deviceRepositry;
		}

		public async Task<Response<DeviceDto>> Handle(DeviceByIdQuery request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepositry.FindbyAsync(x => x.Id ==  request.Id);
            return device == null
                ? Response.Fail("Error: No device found.", new DeviceDto())
                : Response.Ok(_mapper.Map<DeviceDto>(device));
        }
    }
}