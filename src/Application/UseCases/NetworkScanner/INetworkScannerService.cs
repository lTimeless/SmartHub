using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	/// <summary>
	/// Service for network scanning
	/// </summary>
	public interface INetworkScannerService
	{
		/// <summary>
		/// Scans the network for devices
		/// </summary>
		/// <returns>A list with devices found in the network</returns>
		Task<List<NetworkDevice>> SearchNetworkDevicesAsync();
	}
}
