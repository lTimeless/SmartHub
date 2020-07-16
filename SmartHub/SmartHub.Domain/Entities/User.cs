using System;
using Microsoft.AspNetCore.Identity;
using NodaTime;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	/// <inheritdoc cref="Microsoft.AspNetCore.Identity.IdentityUser" />
	public class User : IdentityUser<string>, IEntity
	{
		public Instant CreatedAt { get; set; }
		public Instant LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }
		public string? PersonInfo { get; }

		public virtual PersonName? PersonName { get; }

		public string? HomeId { get; private set; }
		public virtual Home? Home { get; }

		protected User()
		{
		}

		public User(string userName, string? personInfo, PersonName? fullname, Home? home) :
			base(userName)
		{
			Id = Guid.NewGuid().ToString();
			EmailConfirmed = true;
			PersonInfo = personInfo;
			PersonName = fullname;

			Home = home;
		}


	}
}
