using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices.Create
{
    public class DeviceCreateHandler : IRequestHandler<DeviceCreateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceCreateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(DeviceCreateCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();

            var newDevice = new Device(request.Name, request.Description, request.Ipv4, request.CompanyName,
                request.PrimaryConnection, request.SecondaryConnection,
                request.PluginName, request.PluginTypes);

            if (string.IsNullOrEmpty(request.GroupName))
            {
                request.GroupName = DefaultNames.DefaultGroup;
            }
            home?.AddDevice(newDevice, request.GroupName);
            return Response.Ok<string>($"Created new Device with name {newDevice.Name}");
        }
    }
}