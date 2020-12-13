// unset

using HotChocolate;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.UseCases.Entity.Devices
{
	/// <summary>
	/// Device create input.
	/// </summary>
	public record CreateDeviceInput(string Name,
		string? Description,
		string Ipv4,
		string CompanyName,
		string PluginName,
		PluginTypes PluginTypes,
		ConnectionTypes PrimaryConnection, ConnectionTypes SecondaryConnection, string GroupName);

	/// <summary>
	///Device update input.
	/// </summary>
	public record UpdateDeviceInput(string Id,
		Optional<string> Name,
		Optional<string> Description,
		Optional<string> Ipv4,
		Optional<ConnectionTypes> PrimaryConnection,
		Optional<ConnectionTypes> SecondaryConnection,
		Optional<string> GroupName);
}
