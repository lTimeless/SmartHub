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
		public PersonName PersonName { get; private set; } = default!;
		public virtual Home? Home { get; }

		// Needed for ef core
		protected User()
		{
		}

		public User(string userName, string personInfo, PersonName? fullname = default) :
			base(userName)
		{
			Id = Guid.NewGuid().ToString();
			EmailConfirmed = true;
			PersonInfo = personInfo;
			PersonName = fullname ?? new PersonName("","", "");
			Home = null;
		}

		public void UpdateUsername(string fName,string mName, string lname)
		{
			PersonName = new PersonName(fName, mName, lname);
		}

	}
}
