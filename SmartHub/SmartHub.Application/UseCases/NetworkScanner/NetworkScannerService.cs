using Serilog;
using SmartHub.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	public class NetworkScannerService : INetworkScannerService
	{
		private const int _startIpNumber = 1;
		private const int _stopIpNumber = 255;
		private const int _timeout = 500;

		private List<NetworkDeviceResponseDto> _foundDevices;
		private Stopwatch _stopwatch;
		private readonly IPingService _pingService;

		public NetworkScannerService(IPingService pingService)
		{
			_pingService = pingService;
			_foundDevices = new List<NetworkDeviceResponseDto>();
			_stopwatch = new Stopwatch();
		}

		public async Task<List<NetworkDeviceResponseDto>> SearchNetworkDevicesAsync()
		{
			var ownIp = FindMyNetworkGateway();

			var splitIp = ownIp.Split(new[] { '.' });
			var baseIp = $"{splitIp[0]}.{splitIp[1]}.{splitIp[2]}.";

			await RunPingSweepAsync(baseIp).ConfigureAwait(false);
			return _foundDevices;
		}

		private async Task RunPingSweepAsync(string baseIp)
		{
			var tasks = new List<Task>();
			_stopwatch.Start();
			for (var i = _startIpNumber; i <= _stopIpNumber; i++)
			{
				var newIp = baseIp + i;
				var task = PingAndUpdateAsync(newIp);
				tasks.Add(task);
			}

			await Task.WhenAll(tasks).ContinueWith(_ =>
			{
				_stopwatch.Stop();
				var timeSpan = _stopwatch.Elapsed;
				Log.Information($"{_foundDevices.Count} Devices found in {timeSpan}");
			}).ConfigureAwait(false);
		}

		private async Task PingAndUpdateAsync(string ip)
		{
			var reply = await _pingService.Ping(ip, _timeout).ConfigureAwait(false);
			if (reply.Status == IPStatus.Success)
			{
				var hostName = await GetHostnameAsync(ip).ConfigureAwait(false);
				var macAddress = await GetMacAddressAsync(ip).ConfigureAwait(false);
				var newFoundDevice = new NetworkDeviceResponseDto()
				{
					Name = MakeNameFromHostname(hostName),
					Hostname = hostName ?? "Not available",
					MacAddress = macAddress ?? "Not available",
					Ipv4 = ip ?? "Not available",
					Ipv6 = reply.Address.MapToIPv6().ToString()
				};
				_foundDevices.Add(newFoundDevice);
			}
		}

		// Helpers
		private static string MakeNameFromHostname(string hostname)
		{
			if (hostname == null)
			{
				return "Not available";
			}
			if (hostname == "fritz.box" || hostname == "speedport.ip")
			{
				return hostname;
			}

			if (hostname.Contains(".fritz.box"))
			{
				return hostname.Split(new[] {".fritz.box"}, StringSplitOptions.None)[0];
			}
			if (hostname.Contains(".speedport.ip"))
			{
				return hostname.Split(new[] {".speedport.ip"}, StringSplitOptions.None)[0];
			}

			return "Not available";

		}

		private static string FindMyNetworkGateway()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			return host
				.AddressList
				.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
				.ToString() ?? "";
		}

		private static async Task<string> GetHostnameAsync(string ip)
		{
			IPHostEntry? res = null;
			try
			{
				res = await Dns.GetHostEntryAsync(IPAddress.Parse(ip));
			}
			catch (SocketException e)
			{
				Log.Information($"[GetHostname] {e.Message}");
			}
			return res?.HostName ?? "";
		}

		private static async Task<string> GetMacAddressAsync(string ipAddress)
		{
			try
			{
				var process = new Process
				{
					StartInfo =
					{
						FileName = "arp",
						Arguments = "-a " + ipAddress,
						CreateNoWindow = true
					}
				};
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.Start();
				var strOutput = await process.StandardOutput.ReadToEndAsync();
				var substrings = strOutput.Split('-');
				if (substrings.Length < 8)
				{
					return "OWN Machine";
				}

				var macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
								 + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
								 + "-" + substrings[7] + "-"
								 + substrings[8].Substring(0, 2);
				return macAddress;
			}
			catch (Exception e)
			{
				Log.Warning($"[Mac] Error {e.Message}");
			}

			return null;
		}
	}
}
