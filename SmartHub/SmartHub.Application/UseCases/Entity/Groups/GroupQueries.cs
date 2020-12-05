using HotChocolate;
using HotChocolate.Data;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;
using System.Linq;

namespace SmartHub.Application.UseCases.Entity.Groups
{
	/// <summary>
	/// Endpoint for all group queries.
	/// </summary>
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
	}
}