using HotChocolate.Types;
using SmartHub.Application.UseCases.Entity.Devices;
using SmartHub.Application.UseCases.Entity.Groups;

namespace SmartHub.Api.GraphQl
{
	public class RootQueryType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("AppQueries");
			descriptor.Description("Main entrypoint for all queries.");

			descriptor.Include<GroupQueries>();
			descriptor.Include<DeviceQueries>();

		}
	}
}