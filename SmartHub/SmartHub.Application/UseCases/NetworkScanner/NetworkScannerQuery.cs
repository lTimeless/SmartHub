using MediatR;
using System.Collections.Generic;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	public class NetworkScannerQuery : IRequest<List<NetworkDeviceResponseDto>>
	{
	}
}
