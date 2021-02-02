using Microsoft.AspNetCore.Builder;
using Serilog;
using SmartHub.WebUI.Middleware;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SmartHub.WebUI.Extensions
{
	public static class AppExtension
	{
		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}

		public static void ShowLocalIpv4()
		{
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
				Log.ForContext(typeof(AppExtension)).Information($"Your server ip is : {ip.Address}");
			}
		}
	}
}
