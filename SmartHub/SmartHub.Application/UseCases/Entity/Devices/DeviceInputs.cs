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
		string? GroupName,
		PluginTypes PluginTypes,
		ConnectionTypes PrimaryConnection, ConnectionTypes SecondaryConnection);

	/// <summary>
	///Device update input.
	/// </summary>
	public record UpdateDeviceInput(string Id,
		string? Name,
		string? Description,
		string? Ipv4,
		string? GroupName,
		ConnectionTypes? PrimaryConnection,
		ConnectionTypes? SecondaryConnection);
}
