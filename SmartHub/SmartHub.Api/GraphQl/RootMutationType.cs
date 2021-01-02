using HotChocolate.Types;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;
using SmartHub.Application.UseCases.Entity.Users;
using SmartHub.Application.UseCases.Identity;
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
				.Authorize()
				.Description("All mutations for group entity.");
			// Device
			descriptor.Include<DeviceMutations>()
				.Authorize()
				.Description("All mutations for device entity.");
			// Initialization
			descriptor.Include<InitMutations>()
				.Description("All mutations for the initialization services.");
			// Identity
			descriptor.Include<IdentityMutations>()
				.Description("All mutations for the identity services.");
			// User
			descriptor.Include<UserMutations>()
				.Authorize()
				.Description("All mutations for the users entity.");
		}
	}
}