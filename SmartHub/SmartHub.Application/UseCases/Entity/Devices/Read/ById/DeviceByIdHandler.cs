using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Extensions;

namespace SmartHub.Application.UseCases.Entity.Devices.Read.ById
{
    public class DeviceByIdHandler : IRequestHandler<DeviceByIdQuery, Response<DeviceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DeviceDto>> Handle(DeviceByIdQuery request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail("Error: No home created yet.", new DeviceDto());
            }

            var device = home.FindDevice(request.Id);
            return device == null
                ? Response.Fail("Error: No device found.", new DeviceDto())
                : Response.Ok(_mapper.Map<DeviceDto>(device));
        }
    }
}