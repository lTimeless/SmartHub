using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Patch
{
	public class HomePatchCommand : IRequest<Response<HomeDto>>
	{
		public JsonPatchDocument<HomeDto> homePatch { get; set; }

		public HomePatchCommand(JsonPatchDocument<HomeDto> homePatch)
		{
			this.homePatch = homePatch;
		}
	}
}
