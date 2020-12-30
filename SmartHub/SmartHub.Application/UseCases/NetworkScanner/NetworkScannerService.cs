using Serilog;
using SmartHub.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	/// <inheritdoc cref="INetworkScannerService"/>
	public class NetworkScannerService : INetworkScannerService
	{
		private const int _startIpNumber = 1;
		private const int _stopIpNumber = 255;
		private const int _timeout = 500;

		private readonly List<NetworkDevice> _foundDevices;
		private readonly Stopwatch _stopwatch;
		private readonly IPingService _pingService;
		private readonly ILogger _log = Log.ForContext(typeof(NetworkScannerService));

		public NetworkScannerService(IPingService pingService)
		{
			_pingService = pingService;
			_foundDevices = new List<NetworkDevice>();
			_stopwatch = new Stopwatch();
		}

		/// <inheritdoc cref="INetworkScannerService.SearchNetworkDevicesAsync"/>
		public async Task<List<NetworkDevice>> SearchNetworkDevicesAsync()
		{
			var ownIp = NetworkScannerUtils.FindMyNetworkGateway();

			var splitIp = ownIp.Split(new[] { '.' });
			var baseIp = $"{splitIp[0]}.{splitIp[1]}.{splitIp[2]}.";

			await RunPingSweepAsync(baseIp);
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
				_log.Information($"{_foundDevices.Count} Devices found in {timeSpan}");
			});
		}

		private async Task PingAndUpdateAsync(string ip)
		{
			var reply = await _pingService.Ping(ip, _timeout);
			if (reply.Status == IPStatus.Success)
			{
				var hostName = await NetworkScannerUtils.GetHostnameAsync(ip);
				var macAddress = await NetworkScannerUtils.GetMacAddressAsync(ip);
				var newFoundDevice = new NetworkDevice(
					NetworkScannerUtils.MakeName(hostName, macAddress),
					null, ip,
					reply.Address.MapToIPv6().ToString(),
					hostName,
					macAddress
					);
				_foundDevices.Add(newFoundDevice);
			}
		}
	}
}
