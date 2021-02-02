using System;

namespace SmartHub.Domain.Entities
{
	public interface IEntity
	{
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }
	}

	public abstract class BaseEntity : IEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }

		protected BaseEntity()
		{
		}

		protected BaseEntity(string name, string? description)
		{
			Name = name;
			Description = description;
		}

		#region Methods
		public BaseEntity SetName(string name)
		{
			Name = name;
			return this;
		}

		public BaseEntity SetDescription(string description)
		{
			Description = description;
			return this;
		}

		public override bool Equals(object? obj)
		{
			if (!(obj is BaseEntity baseEntity))
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
				return type.BaseType ?? type;
			}
			return type;
		}

		public static bool operator ==(BaseEntity? a, BaseEntity? b)
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

		public static bool operator !=(BaseEntity? a, BaseEntity? b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, CreatedAt, Name, GetRealType().ToString());
		}

		#endregion
	}
}
