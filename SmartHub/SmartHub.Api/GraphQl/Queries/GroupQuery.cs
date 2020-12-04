using AutoMapper;
using HotChocolate;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Api.GraphQl.Queries
{
	public class GroupQuery
	{
		private readonly IMapper _mapper;

		public GroupQuery(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<List<GroupDto>> GetGroups([Service] IBaseRepositoryAsync<Group> groupRepository)
		{
			return _mapper.Map<List<GroupDto>>(await groupRepository.GetAllAsync()) ;
		}
	}
}