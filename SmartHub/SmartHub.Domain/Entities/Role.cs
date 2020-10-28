using System;
using Microsoft.AspNetCore.Identity;

namespace SmartHub.Domain.Entities
{
	public class Role : IdentityRole<string>, IEntity
	{
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }

		protected Role()
		{
		}

		public string? Description { get; set; }

		public Role(string name, string description) : base(name)
		{
			Id = Guid.NewGuid().ToString();
			Description = description;
			CreatedAt = DateTimeOffset.Now;
			LastModifiedAt = DateTimeOffset.Now;
			CreatedBy = string.Empty;
			LastModifiedBy = string.Empty;
		}

	}
}
