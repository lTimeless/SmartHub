using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Groups
{
    public class GroupGetByIdQuery : IRequest<Response<GroupDto>>
    {
        public string Id { get; set; }

        public GroupGetByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GroupGetByIdHandler : IRequestHandler<GroupGetByIdQuery, Response<GroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupGetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GroupDto>> Handle(GroupGetByIdQuery request, CancellationToken cancellationToken)
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return Response.Fail<GroupDto>("Error: No home created yet.", new GroupDto());
            }

            var group = home.Groups.SingleOrDefault(x => x.Id == request.Id);
            return group == null
                ? Response.Fail<GroupDto>("Error: No group found.", new GroupDto())
                : Response.Ok(_mapper.Map<GroupDto>(group));

        }
    }
}