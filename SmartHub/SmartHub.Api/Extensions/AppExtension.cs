using Hangfire;
using Microsoft.AspNetCore.Builder;
using Serilog;
using SmartHub.Api.Middleware;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SmartHub.Api.Extensions
{
	public static class AppExtension
	{
		public static void ConfigureSwagger(this IApplicationBuilder app)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
		}

		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}

		public static void ConfigureHangfire(this IApplicationBuilder app)
		{
			app.UseHangfireDashboard();
		}

		public static void ShowLocalIpv4(this IApplicationBuilder app)
		{
			foreach (var ip in from item in NetworkInterface.GetAllNetworkInterfaces()
							   let _types = new List<NetworkInterfaceType> { NetworkInterfaceType.Ethernet, NetworkInterfaceType.Wireless80211 }
							   where _types.Contains(item.NetworkInterfaceType) && item.OperationalStatus == OperationalStatus.Up
							   from ip in item.GetIPProperties().UnicastAddresses
							   where ip.Address.AddressFamily == AddressFamily.InterNetwork
							   where ip.Address.ToString().Contains("192.")
							   select ip)
			{
				Log.Information($"Your server ip is : {ip.Address}");
			}
		}
	}
}
