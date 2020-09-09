using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.NetworkScanner;
using System.Threading.Tasks;

namespace SmartHub.Api.Controllers
{
	public class NetworkController : BaseController
	{
		/// <summary>
		/// Scans your network for devices
		/// </summary>
		/// <returns>A List of Devices connected to your home network</returns>
		/// <response code="200">Returns connected devices</response>
		/// <response code="400">Returns bad request if error gets throws</response>
		/// <response code="401">Returns unauthorized</response>
		[HttpGet("scan")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Get(CancellationToken cancellationToken)
		{

			return Ok(await Mediator.Send(new NetworkScannerQuery(), cancellationToken));
		}
	}
}
