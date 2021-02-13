using System;
using Microsoft.AspNetCore.Identity;

namespace SmartHub.Domain.Entities
{
	public class Role : IdentityRole<string>, IEntity
	{
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string CreatedBy { get; set; } = default!;
		public string LastModifiedBy { get; set; } = default!;
		public string? Description { get; }

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
