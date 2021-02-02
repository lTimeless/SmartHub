using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using Xunit;

namespace SmartHub.Application.Tests.UseCases.Entity.Devices.Read
{
    public class DeviceGetHandlerTest
    {
        private readonly IMapper _mapperSubstitute;
        private readonly IBaseRepositoryAsync<Device> _deviceRepositorySubstitute;

		private readonly Device _device;
        // private readonly IReadOnlyCollection<DeviceDto> _deviceDtos;

        // private readonly DeviceGetHandler _deviceGetHandler;

        public DeviceGetHandlerTest()
        {
   //          _device = CreateDevice();
   //          _deviceDtos = new List<DeviceDto>() { new DeviceDto() };
   //
			// _mapperSubstitute = CreateMapperSubstitute(_deviceDtos);
			// _deviceRepositorySubstitute = CreateDeviceRepository(_device);

            // _deviceGetHandler = new DeviceGetHandler(_mapperSubstitute, _deviceRepositorySubstitute);
        }

        [Fact]
        internal async Task Handle_HomeRepository_Called_Async()
        {
            // await _deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);
            //
            // await _deviceRepositorySubstitute.Received().GetAllAsync();
        }

        [Fact]
        internal async Task Handle_Mapper_Called_Async()
        {
            // await _deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);

            List<Device> expectedDevices = new() { _device };

            // _mapperSubstitute.Received().Map<IEnumerable<DeviceDto>>(Arg.Is<IEnumerable<Device>>(devices => devices.SequenceEqual(expectedDevices)));
        }


        [Fact]
        internal async Task Handle_Success_ReturnDeviceDtosFromMapper_Async()
        {
            // Response<IEnumerable<DeviceDto>> response = await _deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);

            // Assert.Same(_deviceDtos, response.Data);
        }

        private static Device CreateDevice()
        {
			return new Device("name",
				"description",
				"192",
				"Mock",
				ConnectionTypes.Http,
				ConnectionTypes.Mqtt,
				"MockLight",
				PluginTypes.Light);
		}

        private static IBaseRepositoryAsync<Device> CreateDeviceRepository(Device device)
        {
			IBaseRepositoryAsync<Device> substitute = Substitute.For<IBaseRepositoryAsync<Device>>();
            substitute.GetAllAsync().ReturnsForAnyArgs(new List<Device>() { device });
            return substitute;
        }

        // private static IMapper CreateMapperSubstitute(IReadOnlyCollection<DeviceDto> returnedDeviceDtos)
        // {
        //     var substitute = Substitute.For<IMapper>();
        //     // substitute.Map<IEnumerable<DeviceDto>>(default).ReturnsForAnyArgs(returnedDeviceDtos);
        //     return substitute;
        // }
    }
}