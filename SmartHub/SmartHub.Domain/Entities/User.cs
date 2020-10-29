using System;
using Microsoft.AspNetCore.Identity;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	/// <inheritdoc cref="Microsoft.AspNetCore.Identity.IdentityUser" />
	public class User : IdentityUser<string>, IEntity
	{
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string CreatedBy { get; set; } = default!;
		public string LastModifiedBy { get; set; } = default!;
		public string PersonInfo { get; set; } = default!;
		public PersonName PersonName { get; } = default!;
		public string? HomeId { get; } = default!;
		public virtual Home? Home { get; }

		protected User()
		{
		}

		public User(string userName, string personInfo, PersonName fullname) :
			base(userName)
		{
			Id = Guid.NewGuid().ToString();
			EmailConfirmed = true;
			PersonInfo = personInfo;
			PersonName = fullname;
			Home = null;
		}
	}
}
