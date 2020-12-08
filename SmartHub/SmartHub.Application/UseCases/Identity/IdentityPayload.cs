using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity
{
	public class IdentityPayload : Payload
	{
		public string? Token { get; }
		public User? User { get; }

		public IdentityPayload(User? user, string? token, string? message = null) : base(message)
		{
			Token = token;
			User = user;
		}

		public IdentityPayload(UserError error) : base(new []{ error })
		{
		}
	}
}
