using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Groups
{
    public class GroupGetQuery : IRequest<Response<IEnumerable<GroupDto>>>
    {
    }

    public class GroupGetHandler : IRequestHandler<GroupGetQuery, Response<IEnumerable<GroupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupGetHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GroupDto>>> Handle(GroupGetQuery request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            return home == null
                ? Response.Fail<IEnumerable<GroupDto>>("Error: No home created yet.")
                : Response.Ok(_mapper.Map<IEnumerable<GroupDto>>(home.Groups));
        }
    }
}