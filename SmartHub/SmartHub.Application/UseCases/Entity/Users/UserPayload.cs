using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Users
{
	public class UserPayload : Payload
	{
		public User User { get; }

		public UserPayload(User? user, string? message = null) : base(message)
		{
			User = user;
		}

		public UserPayload(UserError error) : base(new []{ error })
		{
		}
	}
}