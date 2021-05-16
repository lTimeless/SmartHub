using System;
using Microsoft.AspNetCore.Identity;
using SmartHub.Domain.Entities.ValueObjects;

namespace SmartHub.Domain.Entities
{
	/// <inheritdoc cref="IdentityUser" />
	public class User : IdentityUser<string>, IEntity
	{
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string CreatedBy { get; set; } = default!;
		public string LastModifiedBy { get; set; } = default!;

		/// <summary>
		/// Determines if the user hasn't logged in yet.
		/// true = user hasn't logged in at least once
		/// </summary>
		public bool IsFirstLogin { get; set; }

		public string PersonInfo { get; set; } = default!;
		public PersonName PersonName { get; } = default!;

		protected User()
		{
		}

		public User(string userName, string personInfo, PersonName? fullname = default) :
			base(userName)
		{
			Id = Guid.NewGuid().ToString();
			EmailConfirmed = true;
			PersonInfo = personInfo;
			PersonName = fullname ?? new PersonName("", "", "");
			IsFirstLogin = true;
		}
	}
}