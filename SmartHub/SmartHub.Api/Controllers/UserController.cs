using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Application.UseCases.Entity.Users.Put;

namespace SmartHub.Api.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Retrieves a user for the given id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>A userDto</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByName(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Full updates the User
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserPutCommand value)
        {
            return Ok(await Mediator.Send(value));
        }

    }
}