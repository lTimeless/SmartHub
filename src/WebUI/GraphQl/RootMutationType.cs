using HotChocolate.Types;
using SmartHub.Application.UseCases.Entity.Devices.Mutations;
using SmartHub.Application.UseCases.Entity.Groups.Mutations;
using SmartHub.Application.UseCases.Entity.Users.Mutations;
using SmartHub.Application.UseCases.Identity.Mutations;
using SmartHub.Application.UseCases.Init;

namespace SmartHub.WebUI.GraphQl
{
	/// <summary>
	/// Root mutation type.
	/// </summary>
	public class RootMutationType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("AppMutations")
				.Description("Main entrypoint for all mutations.");

			// Identity
			descriptor.Include<LoginIdentityMutation>()
				.Include<RefreshIdentityMutation>()
				.Include<RegistrationIdentityMutation>();
			// Initialization
			descriptor.Include<InitMutations>();
			// Group
			descriptor.Include<CreateGroupMutation>()
				.Include<UpdateGroupMutation>();
			// Device
			descriptor.Include<CreateDeviceMutation>()
				.Include<UpdateDeviceMutation>();
			// User
			descriptor.Include<UpdateUserMutations>();
		}
	}
}