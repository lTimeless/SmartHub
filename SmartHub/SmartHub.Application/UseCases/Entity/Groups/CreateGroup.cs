using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{

    public class GroupCreateCommand : IRequest<Response<GroupDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        // public List<DeviceDto> DeviceDtos { get; set; }

        public GroupCreateCommand(string description, string name)
        {
            Description = description;
            Name = name;
            // DeviceDtos = deviceDtos;
        }
    }

    public class GroupCreateHandler : IRequestHandler<GroupCreateCommand, Response<GroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupCreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GroupDto>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail<GroupDto>("Error: No home created yet.");
            }
            var newGroup = new Group(request.Name, request.Description, home);
            home.AddGroup(newGroup);
            return Response.Ok(_mapper.Map<GroupDto>(newGroup));
        }
    }
}