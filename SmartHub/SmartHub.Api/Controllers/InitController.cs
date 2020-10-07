using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.InitCheck.CheckHome;
using SmartHub.Application.UseCases.InitCheck.CheckUsers;

namespace SmartHub.Api.Controllers
{
    public class InitController : BaseController
    {
        /// <summary>
        /// Gets an indication if a home exist
        /// </summary>
        /// <returns>bool true= exist</returns>
        [HttpGet("checkHome")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHome()
        {
            return Ok(await Mediator.Send(new CheckHomeQuery()).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets an indication if users exist
        /// </summary>
        /// <returns>bool true= exist</returns>
        [HttpGet("checkUsers")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Mediator.Send(new CheckUsersQuery()).ConfigureAwait(false));
        }
    }
}