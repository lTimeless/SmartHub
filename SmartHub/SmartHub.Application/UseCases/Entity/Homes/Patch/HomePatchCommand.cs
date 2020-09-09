using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Patch
{
	public class HomePatchCommand : IRequest<Response<HomeDto>>
	{
		public JsonPatchDocument<HomeDto> HomePatch { get; set; }

		public HomePatchCommand(JsonPatchDocument<HomeDto> homePatch)
		{
			this.HomePatch = homePatch;
		}
	}
}
