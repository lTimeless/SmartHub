using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Identity
{
	public class IdentityPayload : Payload
	{
		public bool IsAuthenticated { get; }
		public User? User { get; }

		public IdentityPayload(User? user , string? message = null) : base(message)
		{
			User = user;
			IsAuthenticated = true;
		}

		public IdentityPayload(UserError error) : base(new[] {error})
		{
			IsAuthenticated = false;
		}
	}
}