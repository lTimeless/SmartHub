using System;
using NodaTime;

namespace SmartHub.Domain.Entities
{
	public interface IEntity
	{
		public Instant CreatedAt { get; set; }
		public Instant LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }
	}


	public abstract class Entity : IEntity
	{
		public string Id { get; }
		public Instant CreatedAt { get; set; }
		public Instant LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }

		public string Name { get; protected set; }
		public string? Description { get; protected set; }

		protected Entity()
		{
		}

		protected Entity(string name, string? description)
		{
			Id = Guid.NewGuid().ToString();
			Name = name;
			Description = description;
		}

		public override bool Equals(object? obj)
		{
			if (!(obj is Entity baseEntity))
			{
				return false;
			}

			if (ReferenceEquals(this, baseEntity))
			{
				return true;
			}
			if (GetRealType() != baseEntity.GetRealType())
			{
				return false;
			}
			if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(baseEntity.Id))
			{
				return false;
			}
			return Id == baseEntity.Id;
		}

		private Type GetRealType()
		{
			var type = GetType();

			if (type.ToString().Contains("Castle.Proxies"))
			{
				return type.BaseType;
			}
			return type;
		}

		public static bool operator ==(Entity a, Entity b)
		{
			if (a is null && b is null)
			{
				return true;
			}
			if (a is null || b is null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(Entity a, Entity b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, CreatedAt, Name, GetRealType().ToString());
		}
	}
}
