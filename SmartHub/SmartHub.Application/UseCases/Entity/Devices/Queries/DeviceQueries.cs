using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Domain.Entities;
using System.Linq;

namespace SmartHub.Application.UseCases.Entity.Devices.Queries
{
	/// <summary>
	/// Endpoint for all device queries.
	/// </summary>
	[Authorize]
	[ExtendObjectType(Name = "RootQueries")]
	[GraphQLDescription("All queries for the DeviceEntity.")]
	public class DeviceQueries
	{
		/// <summary>
		/// Retrieves devices via projection. Filtering and Sorting is also possible.
		/// </summary>
		/// <param name="deviceRepository">The device repository.</param>
		/// <returns>Returns all/one/null devices and/or filtered and or sorted.</returns>
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Device> GetDevices([Service] IBaseRepositoryAsync<Device> deviceRepository)
		{
			return deviceRepository.GetAllAsQueryable();
		}

		public int GetDevicesCount([Service] IBaseRepositoryAsync<Device> deviceRepository)
		{
			return deviceRepository.GetAllAsQueryable().Count();
		}
	}
}