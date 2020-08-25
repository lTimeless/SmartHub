
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Homes.Create;

namespace SmartHub.Api.Controllers
{
    public class InitController : BaseController
    {
        /// <summary>
        /// Triggers the initialization for SmartHub
        /// </summary>
        /// <returns>The Home</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody]HomeCreateCommand createCommand)
        {
            return Ok(await Mediator.Send(createCommand).ConfigureAwait(false));
        }
    }
}