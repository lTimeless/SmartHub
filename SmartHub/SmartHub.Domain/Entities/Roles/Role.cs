using System;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace SmartHub.Domain.Entities.Roles
{
	public class Role : IdentityRole<string>, IEntity
	{
		public Instant CreatedAt { get; set; }
		public Instant LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }

		public string? Description { get; set; }

		protected Role()
		{
		}

		public Role(string name, string description) : base(name)
		{
			Id = Guid.NewGuid().ToString();
			Description = description;
		}

	}
}
