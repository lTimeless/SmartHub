using Microsoft.AspNetCore.Identity;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.ValueObjects;
using System;

namespace SmartHub.Domain.Entities.Users
{
	public class User : IdentityUser<string>
	{
		private DateTime CreatedAt { get; }
		private DateTime ModifiedDate { get; set; }

		public string? PersonInfo { get; private set; }

		public virtual PersonName? PersonName { get; private set; }

		public string? HomeId { get; private set; }
		public virtual Home? Home { get; private set; }

		protected User()
		{
		}

		public User(string userName, string? personInfo, PersonName? fullname, Home? home) :
			base(userName)
		{
			Id = Guid.NewGuid().ToString();
			CreatedAt = DateTime.Now;
			ModifiedDate = DateTime.Now;

			EmailConfirmed = true;
			PersonInfo = personInfo;
			PersonName = fullname;

			Home = home;
		}

		public void UpdateModifiedDate()
		{
			ModifiedDate = DateTime.Now;
		}
	}
}
