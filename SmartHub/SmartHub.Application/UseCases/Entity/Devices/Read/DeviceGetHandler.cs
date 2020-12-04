using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices.Read
{
	public class DeviceGetHandler  : IRequestHandler<DeviceGetQuery, Response<IEnumerable<DeviceDto>>>
    {
		private readonly IBaseRepositoryAsync<Device> _deviceRepository;
        private readonly IMapper _mapper;

		public DeviceGetHandler(IMapper mapper, IBaseRepositoryAsync<Device> deviceRepository)
		{
			_mapper = mapper;
			_deviceRepository = deviceRepository;
		}

		public async Task<Response<IEnumerable<DeviceDto>>> Handle(DeviceGetQuery request, CancellationToken cancellationToken)
        {
            var devices = await _deviceRepository.GetAllAsync();
            return Response.Ok(_mapper.Map<IEnumerable<DeviceDto>>(devices));
        }
    }
}