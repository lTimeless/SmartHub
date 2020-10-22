using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Devices.Read;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using Xunit;

namespace SmartHub.Application.Tests.UseCases.Entity.Devices.Read
{
    public class DeviceGetHandlerTest
    {
        private readonly IMapper mapperSubstitute;
        private readonly IHomeRepository homeRepositorySubstitute;

        private readonly Home home;
        private readonly IReadOnlyCollection<DeviceDto> deviceDtos;

        private readonly DeviceGetHandler deviceGetHandler;

        public DeviceGetHandlerTest()
        {
            home = CreateHome();
            deviceDtos = home.Groups.SelectMany(x => x.Devices).Select(device => new DeviceDto()).ToArray();

            mapperSubstitute = CreateMapperSubstitute(deviceDtos);
            homeRepositorySubstitute = CreateHomeRepository(home);
            IUnitOfWork unitOfWorkSubstitute = CreateUnitOfWorkSubstitute(homeRepositorySubstitute);

            deviceGetHandler = new DeviceGetHandler(mapperSubstitute, unitOfWorkSubstitute);
        }

        [Fact]
        internal async Task Handle_HomeRepository_Called_Async()
        {
            await deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);

            await homeRepositorySubstitute.Received().GetHome();
        }

        [Fact]
        internal async Task Handle_Mapper_Called_Async()
        {
            await deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);

            IEnumerable<Device> expectedDevices = home.Groups.SelectMany(x => x.Devices);

            mapperSubstitute.Received().Map<IEnumerable<DeviceDto>>(Arg.Is<IEnumerable<Device>>(devices => devices.SequenceEqual(expectedDevices)));
        }

        [Fact]
        internal async Task Handle_NoHome_ReturnFailResponse_Async()
        {
            homeRepositorySubstitute.GetHome().ReturnsForAnyArgs(Task.FromResult<Home>(null!));
            Response<IEnumerable<DeviceDto>> response = await deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);

            Assert.False(response.Success);
        }

        [Fact]
        internal async Task Handle_Success_ReturnDeviceDtosFromMapper_Async()
        {
            Response<IEnumerable<DeviceDto>> response = await deviceGetHandler.Handle(new DeviceGetQuery(), CancellationToken.None);

            Assert.Same(deviceDtos, response.Data);
        }

        private static Home CreateHome()
        {
            Home home = new Home("name", "description");
            Group group = new Group("groupName", "description");

            group
                .AddDevice(new Device("name1", default, "ip", "company", ConnectionTypes.None, default, "pluginName", default))
                .AddDevice(new Device("name2", default, "ip", "company", ConnectionTypes.None, default, "pluginName", default));

            home.AddGroup(group);

            return home;
        }

        private static IUnitOfWork CreateUnitOfWorkSubstitute(IHomeRepository homeRepository)
        {
            IUnitOfWork substitute = Substitute.For<IUnitOfWork>();
            substitute.HomeRepository.Returns(homeRepository);
            return substitute;
        }

        private static IHomeRepository CreateHomeRepository(Home home)
        {
            IHomeRepository substitute = Substitute.For<IHomeRepository>();
            substitute.GetHome().ReturnsForAnyArgs(home);
            return substitute;
        }

        private static IMapper CreateMapperSubstitute(IReadOnlyCollection<DeviceDto> deviceDtos)
        {
            var substitute = Substitute.For<IMapper>();
            substitute.Map<IEnumerable<DeviceDto>>(default).ReturnsForAnyArgs(deviceDtos);
            return substitute;
        }
    }
}