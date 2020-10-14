using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Devices.Create;
using SmartHub.Application.UseCases.Entity.Devices.Read;
using SmartHub.Application.UseCases.Entity.Devices.Read.ById;
using SmartHub.Application.UseCases.Entity.Devices.Update;

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
        /// Get device by id
        /// </summary>
        /// <returns>All groups</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new DeviceByIdQuery(id)).ConfigureAwait(false));
        }

        /// <summary>
        /// Create a device
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] DeviceCreateCommand value)
        {
            var response = await Mediator.Send(value).ConfigureAwait(false);
            return Created("Post", response);
        }

        /// <summary>
        /// Update a device
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] DeviceUpdateCommand value)
        {
            var response = await Mediator.Send(value).ConfigureAwait(false);
            return Ok(response);
        }
    }
}