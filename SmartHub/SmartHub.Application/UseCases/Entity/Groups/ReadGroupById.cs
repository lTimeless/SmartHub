using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
    public record GroupGetByIdQuery : IRequest<Response<GroupDto>>
    {
        public string Id { get; init; }
    }

    public class GroupGetByIdHandler : IRequestHandler<GroupGetByIdQuery, Response<GroupDto>>
    {
        private readonly IMapper _mapper;
		private readonly IBaseRepositoryAsync<Group> _grouprepository;

		public GroupGetByIdHandler(IMapper mapper, IBaseRepositoryAsync<Group> grouprepository)
		{
			_mapper = mapper;
			_grouprepository = grouprepository;
		}

		public async Task<Response<GroupDto>> Handle(GroupGetByIdQuery request, CancellationToken cancellationToken)
        {

			var group = await _grouprepository.FindbyAsync(x => x.Id == request.Id);
            return group == null
                ? Response.Fail("Error: No group found.", new GroupDto())
                : Response.Ok(_mapper.Map<GroupDto>(group));

        }
    }
}