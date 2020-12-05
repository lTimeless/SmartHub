using HotChocolate.Types;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;

namespace SmartHub.Api.GraphQl
{
	public class RootMutationType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("AppMutations");
			descriptor.Description("Main entrypoint for all mutations.");

			// Group
			descriptor.Include<GroupMutations>()
				.Description("All Mutations for GroupEntity.");
			// Device
			descriptor.Include<DeviceMutations>()
				.Description("All Mutations for DeviceEntity.");

		}
	}
}