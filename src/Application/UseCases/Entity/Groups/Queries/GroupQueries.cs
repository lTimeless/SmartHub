using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;
using System.Linq;

namespace SmartHub.Application.UseCases.Entity.Groups.Queries
{
	/// <summary>
	/// Endpoint for all group queries.
	/// </summary>
	[Authorize]
	[ExtendObjectType(Name = "RootQueries")]
	[GraphQLDescription("All queries for the GroupEntity.")]
	public class GroupQueries
	{
		/// <summary>
		/// Retrieves groups via projection. Filtering and Sorting is also possible.
		/// </summary>
		/// <param name="groupsRepository">The group repository.</param>
		/// <returns>Returns all/one/null groups and/or filtered and or sorted.</returns>
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Group> GetGroups([Service] IBaseRepositoryAsync<Group> groupsRepository)
		{
			return groupsRepository.GetAllAsQueryable();
		}

		/// <summary>
		/// Gets the current count of all groups
		/// </summary>
		/// <param name="groupsRepository"></param>
		/// <returns></returns>
		public int GetGroupsCount([Service] IBaseRepositoryAsync<Group> groupsRepository)
		{
			return groupsRepository.GetAllAsQueryable().Count();
		}
	}
}