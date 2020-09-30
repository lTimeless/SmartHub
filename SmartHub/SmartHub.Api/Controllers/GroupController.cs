using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Groups;

namespace SmartHub.Api.Controllers
{
    public class GroupController : BaseController
    {
        /// <summary>
        /// Get all groups
        /// </summary>
        /// <returns>All groups</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GroupGetQuery()).ConfigureAwait(false));
        }

        /// <summary>
        /// Get all groups
        /// </summary>
        /// <returns>All groups</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GroupGetByIdQuery(id)).ConfigureAwait(false));
        }

        /// <summary>
        /// Create a group
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] GroupCreateCommand value)
        {
            var response = await Mediator.Send(value).ConfigureAwait(false);
            return CreatedAtAction("Post", response.Data.Id, response);
        }

        /// <summary>
        /// Update a group
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put( [FromBody] GroupUpdateCommand value)
        {
            var response = await Mediator.Send(value).ConfigureAwait(false);
            return Ok(response);
        }
    }
}