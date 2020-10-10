using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Devices.Read
{
    public class DeviceGetHandler  : IRequestHandler<DeviceGetQuery, Response<IEnumerable<DeviceDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceGetHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<DeviceDto>>> Handle(DeviceGetQuery request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail<IEnumerable<DeviceDto>>("Error: No home created yet.");
            }
            var devices = home.Groups.SelectMany(x => x.Devices);
            return Response.Ok(_mapper.Map<IEnumerable<DeviceDto>>(devices));
        }
    }
}