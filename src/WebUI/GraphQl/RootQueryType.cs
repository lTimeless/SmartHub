using HotChocolate.Types;
using SmartHub.Application.UseCases.DeviceState.Queries;
using SmartHub.Application.UseCases.Entity.Devices.Queries;
using SmartHub.Application.UseCases.Entity.Groups.Queries;
using SmartHub.Application.UseCases.Identity.Queries;
using SmartHub.Application.UseCases.Init;
using SmartHub.Application.UseCases.NetworkScanner;

namespace SmartHub.WebUI.GraphQl
{
	/// <summary>
	/// Root query type.
	/// </summary>
	public class RootQueryType : ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("AppQueries")
				.Description("Main entrypoint for all queries.");
			// Group
			descriptor.Include<GroupQueries>();
			// Device
			descriptor.Include<DeviceQueries>();
			// Identity
			descriptor.Include<IdentityQueries>();
			// Initialization
			descriptor.Include<InitQueries>();
			// NetworkScanner
			descriptor.Include<NetworkScannerQueries>();
			// DeviceState
			descriptor.Include<DeviceLightStateQueries>();
		}
	}
}