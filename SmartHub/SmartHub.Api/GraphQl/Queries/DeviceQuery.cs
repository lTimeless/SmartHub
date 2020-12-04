using AutoMapper;
using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Api.GraphQl.Queries
{
	public class DeviceQuery
	{
		private readonly IMapper _mapper;

		public DeviceQuery(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<List<DeviceDto>> GetDevices([Service] IBaseRepositoryAsync<Device> deviceRepository)
		{
			return _mapper.Map<List<DeviceDto>>(await deviceRepository.GetAllAsync()) ;
		}
	}
}