using System.Collections.Generic;
using System.Linq;

namespace SmartHub.Domain.Entities.ValueObjects
{
	public abstract class ValueObject
	{
		protected abstract IEnumerable<object> GetEqualityComponents();

		public override bool Equals(object? obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (GetType() != obj.GetType())
			{
				return false;
			}
			var valueObjectt = (ValueObject)obj;

			return GetEqualityComponents().SequenceEqual(valueObjectt.GetEqualityComponents());
		}

		public override int GetHashCode()
		{
			return GetEqualityComponents()
			 .Select(x => x != null ? x.GetHashCode() : 0)
			 .Aggregate((x, y) => x ^ y);
		}

		public static bool operator ==(ValueObject a, ValueObject b)
		{
			if (a is null && b is null)
			{
				return false;
			}
			return a is null || a.Equals(b);
		}

		public static bool operator !=(ValueObject a, ValueObject b)
		{
			return !(a == b);
		}

		public static string Print
		{
			get
			{
				return typeof(ValueObject).GetProperties().Aggregate(string.Empty,
					(current, item) => current + ", " + item.GetValue(item, null));
			}
		}
	}
}
