// unset

using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	/// <summary>
	/// Endpoint for all network scanner queries.
	/// </summary>
	[Authorize]
	[ExtendObjectType(Name = "RootQueries")]
	[GraphQLDescription("All queries for network scanning operations.")]
	public class NetworkScannerQueries
	{
		/// <summary>
		/// Retrieves devices that are available on the network.
		/// </summary>
		/// <param name="networkScannerService">The service to scan the network.</param>
		/// <returns>Returns found network devices.</returns>
		[UseFiltering]
		[UseSorting]
		public async Task<IEnumerable<NetworkDevice>> ScanNetworkDevices([Service] INetworkScannerService networkScannerService)
		{
			return await networkScannerService.SearchNetworkDevicesAsync();
		}
	}
}