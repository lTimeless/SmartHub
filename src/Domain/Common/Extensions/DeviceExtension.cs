using SmartHub.Domain.Common.Enums;
using SmartHub.Domain.Entities;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Common.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="Device"/> entity.
	/// </summary>
	public static class DeviceExtension
	{
		/// <summary>
		/// Sets the Ip.
		/// </summary>
		/// <param name="device">This device entity.</param>
		/// <param name="ip">The Ip.</param>
		/// <returns></returns>
		public static Device SetIp(this Device device, string ip)
		{
			device.Ip = new IpAddress(ip);
			return device;
		}

		/// <summary>
		/// Sets the connectionTypes.
		/// </summary>
		/// <param name="device">This device entity.</param>
		/// <param name="primary">The primary connectionType.</param>
		/// <param name="secondary">The secondary connectionType.</param>
		/// <returns></returns>
		public static Device SetConnectionTypes(this Device device, ConnectionTypes primary, ConnectionTypes secondary)
		{
			device.PrimaryConnection = primary;
			device.SecondaryConnection = secondary;
			return device;
		}
	}
}