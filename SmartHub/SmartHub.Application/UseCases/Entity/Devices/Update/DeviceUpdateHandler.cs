using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Devices.Update
{
    public class DeviceUpdateHandler : IRequestHandler<DeviceUpdateCommand, Response<DeviceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceUpdateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DeviceDto>> Handle(DeviceUpdateCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail<DeviceDto>("Error: No home created yet.");
            }

            var result = home.UpdateDevice(request.Id, request.Name, request.Description, request.Ipv4 ,request.PrimaryConnection, request.SecondaryConnection);
            if (result)
            {
                Response.Fail<DeviceDto>($"Error: Couldn't update device with id {request.Id}.");
            }
            return Response.Ok<DeviceDto>($"Updated device with name {request.Name}", null);
        }
    }
}