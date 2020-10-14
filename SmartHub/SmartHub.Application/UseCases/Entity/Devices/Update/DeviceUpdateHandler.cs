using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Devices.Update
{
    public class DeviceUpdateHandler : IRequestHandler<DeviceUpdateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceUpdateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(DeviceUpdateCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail<string>("Error: No home created yet.");
            }

            var result = home.UpdateDevice(request.Id, request.Name, request.Description, request.Ipv4 ,request.PrimaryConnection, request.SecondaryConnection);
            if (result)
            {
                Response.Fail<string>($"Error: Couldn't update device with id {request.Id}.");
            }
            return Response.Ok<string>($"Updated device with name {request.Name}");
        }
    }
}