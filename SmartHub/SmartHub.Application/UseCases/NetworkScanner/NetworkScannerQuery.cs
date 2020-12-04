using MediatR;
using System.Collections.Generic;

namespace SmartHub.Application.UseCases.NetworkScanner
{
	public record NetworkScannerQuery : IRequest<List<NetworkDeviceResponseDto>>
	{
	}
}
