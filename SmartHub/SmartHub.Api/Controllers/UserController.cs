using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetByName(string id)
        {
            return Ok();
        }
    }
}