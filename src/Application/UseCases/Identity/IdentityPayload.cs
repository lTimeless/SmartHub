using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity
{
	public class IdentityPayload : Payload
	{
		public string? Token { get; }
		// TODO anstatt user dann vlt role(s) und username/id übergeben
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
