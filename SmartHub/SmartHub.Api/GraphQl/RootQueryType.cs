using HotChocolate.Types;
using SmartHub.Api.GraphQl.Queries;

namespace SmartHub.Api.GraphQl
{
	public class RootQueryType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			base.Configure(descriptor);

			descriptor.Name("AppQueries");
			descriptor.Description("Main entrypoint for all queries.");

			descriptor.Include<GroupQuery>();
			descriptor.Include<DeviceQuery>();

		}
	}
}