using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Devices.Read;
using SmartHub.Application.UseCases.Entity.Groups;

namespace SmartHub.Api.Controllers
{
    public class DeviceController : BaseController
    {
        /// <summary>
        /// Get all devices
        /// </summary>
        /// <returns>List of devices</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new DeviceGetQuery()));
        }

        /// <summary>
        /// Create a device
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] GroupCreateCommand value)
        {
            var response = await Mediator.Send(value).ConfigureAwait(false);
            return CreatedAtAction("Post", response.Data.Id, response);
        }
    }
}