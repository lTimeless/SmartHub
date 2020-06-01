using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHub.Api.Controllers
{
	public class TestController : BaseController
	{
		// GET: api/Test
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/Test/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			return Ok();
		}

		// POST: api/Test
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT: api/Test/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
