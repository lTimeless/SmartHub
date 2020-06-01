using System;
using Microsoft.AspNetCore.Identity;

namespace SmartHub.Domain.Entities.Roles
{
	public class Role : IdentityRole<string>
	{
		private DateTime CreatedAt { get; set; }
		private DateTime ModifiedDate { get; set; }
		public string? Description { get; private set; }

		protected Role()
		{
		}

		public Role(string name, string description) : base(name)
		{
			Id = Guid.NewGuid().ToString();
			CreatedAt = DateTime.Now;
			ModifiedDate = DateTime.Now;

			Description = description;
		}

		public void UpdateModifiedDate()
		{
			ModifiedDate = DateTime.Now;
		}
	}
}
