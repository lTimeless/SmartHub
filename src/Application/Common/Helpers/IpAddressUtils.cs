using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SmartHub.Application.Common.Helpers
{
	/// <summary>
	/// Utility functions for IpAddress.
	/// </summary>
	public static class IpAddressUtils
	{
		/// <summary>
		/// Retrieves the ip address of the current computer.
		/// </summary>
		/// <returns>IpAddress.</returns>
		public static IPAddress ShowLocalIpv4()
		{
			IPAddress hostIp = IPAddress.Any;
			foreach (var ip in NetworkInterface.GetAllNetworkInterfaces()
				.Select(item => new
				{
					item,
					types = new List<NetworkInterfaceType>
					{
						NetworkInterfaceType.Ethernet, NetworkInterfaceType.Wireless80211
					}
				})
				.Where(t =>
					t.types.Contains(t.item.NetworkInterfaceType) &&
					t.item.OperationalStatus == OperationalStatus.Up)
				.SelectMany(t => t.item.GetIPProperties().UnicastAddresses, (t, ip) => new {t, ip})
				.Where(t => t.ip.Address.AddressFamily == AddressFamily.InterNetwork)
				.Where(t => t.ip.Address.ToString().Contains("192."))
				.Select(t => t.ip))
			{
				hostIp = ip.Address;
			}
			return hostIp;
		}
	}
}