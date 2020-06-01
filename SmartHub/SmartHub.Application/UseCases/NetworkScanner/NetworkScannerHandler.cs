using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	public class NetworkScannerHandler : IRequestHandler<NetworkScannerQuery, List<NetworkDeviceResponseDto>>
	{
		private readonly INetworkScannerService _networkScannerService;

		public NetworkScannerHandler(INetworkScannerService networkScannerService)
		{
			_networkScannerService = networkScannerService;
		}

		public async Task<List<NetworkDeviceResponseDto>> Handle(NetworkScannerQuery request, CancellationToken cancellationToken)
		{
			return await _networkScannerService.SearchNetworkDevicesAsync().ConfigureAwait(false);
		}
	}
}
