using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
	public record GroupGetQuery : IRequest<Response<IEnumerable<GroupDto>>>
    {
    }

    public class GroupGetHandler : IRequestHandler<GroupGetQuery, Response<IEnumerable<GroupDto>>>
    {
        private readonly IMapper _mapper;
		private readonly IBaseRepositoryAsync<Group> _groupRepository;

		public GroupGetHandler(IMapper mapper, IBaseRepositoryAsync<Group> groupRepository)
		{
			_mapper = mapper;
			_groupRepository = groupRepository;
		}

		public async Task<Response<IEnumerable<GroupDto>>> Handle(GroupGetQuery request, CancellationToken cancellationToken)
        {
			var groups = await _groupRepository.GetAllAsync();
			return Response.Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
        }
    }
}