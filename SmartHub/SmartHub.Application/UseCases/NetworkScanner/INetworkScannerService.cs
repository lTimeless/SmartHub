using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	public interface INetworkScannerService
	{
		Task<List<NetworkDeviceResponseDto>> SearchNetworkDevicesAsync();
	}
}
