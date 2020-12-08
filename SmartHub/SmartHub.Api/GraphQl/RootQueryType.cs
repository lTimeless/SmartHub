using HotChocolate.Types;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Application.UseCases.Identity;
using SmartHub.Application.UseCases.Init;
using SmartHub.Application.UseCases.NetworkScanner;

namespace SmartHub.Api.GraphQl
{
	/// <summary>
	/// Root GraphQL Queries Endpoint.
	/// </summary>
	public class RootQueryType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("AppQueries");
			descriptor.Description("Main entrypoint for all queries.");

			// Group
			descriptor.Include<GroupQueries>()
				.Description("All queries for the GroupEntity.");
			// Device
			descriptor.Include<DeviceQueries>()
				.Description("All queries for the DeviceEntity.");
			// NetworkScanner
			descriptor.Include<NetworkScannerQueries>()
				.Description("All queries for network scanning operations");
			// Initialization
			descriptor.Include<InitQueries>()
				.Description("All queries for the initialization service.");
			// Identity
			descriptor.Include<IdentityQueries>()
				.Description("All queries for the me services.");
		}
	}
}