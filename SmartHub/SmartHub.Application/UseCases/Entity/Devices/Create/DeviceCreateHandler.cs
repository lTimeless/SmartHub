using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Devices.Create
{
    public class DeviceCreateHandler : IRequestHandler<DeviceCreateCommand, Response<DeviceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceCreateHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DeviceDto>> Handle(DeviceCreateCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail<DeviceDto>("Error: No home created yet.");
            }

            var newDevice = new Device(request.Name, request.Description, request.Ipv4, request.CompanyName,
                request.PrimaryConnection, request.SecondaryConnection,
                request.PluginName, request.PluginTypes);

            home.AddDevice(newDevice, request.GroupId);
            return Response.Ok($"Created new Device with name {newDevice.Name}", _mapper.Map<DeviceDto>(newDevice));
        }
    }
}