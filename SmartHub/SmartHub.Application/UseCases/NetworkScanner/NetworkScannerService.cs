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
	/// <inheritdoc cref="INetworkScannerService"/>
	public class NetworkScannerService : INetworkScannerService
	{
		private const int StartIpNumber = 1;
		private const int StopIpNumber = 255;
		private const int Timeout = 500;

		private readonly List<NetworkDeviceResponseDto> _foundDevices;
		private readonly Stopwatch _stopwatch;
		private readonly IPingService _pingService;
		private readonly ILogger _log = Log.ForContext(typeof(NetworkScannerService));

		public NetworkScannerService(IPingService pingService)
		{
			_pingService = pingService;
			_foundDevices = new List<NetworkDeviceResponseDto>();
			_stopwatch = new Stopwatch();
		}

		/// <inheritdoc cref="INetworkScannerService.SearchNetworkDevicesAsync"/>
		public async Task<List<NetworkDeviceResponseDto>> SearchNetworkDevicesAsync()
		{
			var ownIp = NetworkScannerUtils.FindMyNetworkGateway();

			var splitIp = ownIp.Split(new[] { '.' });
			var baseIp = $"{splitIp[0]}.{splitIp[1]}.{splitIp[2]}.";

			await RunPingSweepAsync(baseIp).ConfigureAwait(false);
			return _foundDevices;
		}

		private async Task RunPingSweepAsync(string baseIp)
		{
			var tasks = new List<Task>();
			_stopwatch.Start();
			for (var i = StartIpNumber; i <= StopIpNumber; i++)
			{
				var newIp = baseIp + i;
				var task = PingAndUpdateAsync(newIp);
				tasks.Add(task);
			}

			await Task.WhenAll(tasks).ContinueWith(_ =>
			{
				_stopwatch.Stop();
				var timeSpan = _stopwatch.Elapsed;
				_log.Information($"{_foundDevices.Count} Devices found in {timeSpan}");
			}).ConfigureAwait(false);
		}

		private async Task PingAndUpdateAsync(string ip)
		{
			var reply = await _pingService.Ping(ip, Timeout).ConfigureAwait(false);
			if (reply.Status == IPStatus.Success)
			{
				var hostName = await NetworkScannerUtils.GetHostnameAsync(ip).ConfigureAwait(false);
				var macAddress = await NetworkScannerUtils.GetMacAddressAsync(ip).ConfigureAwait(false);
				var newFoundDevice = new NetworkDeviceResponseDto(
					NetworkScannerUtils.MakeNameFromHostname(hostName),
					null, ip ?? "Not available",
					reply.Address.MapToIPv6().ToString(),
					hostName ?? "Not available", 
					macAddress ?? "Not available"
					);
				_foundDevices.Add(newFoundDevice);
			}
		}
	}
}
