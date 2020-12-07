using HotChocolate.Types;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Application.UseCases.Init;

namespace SmartHub.Api.GraphQl
{
	/// <summary>
	/// Root GraphQL Mutations Endpoint.
	/// </summary>
	public class RootMutationType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("AppMutations");
			descriptor.Description("Main entrypoint for all mutations.");

			// Group
			descriptor.Include<GroupMutations>()
				.Description("All mutations for GroupEntity.");
			// Device
			descriptor.Include<DeviceMutations>()
				.Description("All mutations for DeviceEntity.");
			// Initialization
			descriptor.Include<InitMutations>()
				.Description("All mutations for the initialization service.");

		}
	}
}