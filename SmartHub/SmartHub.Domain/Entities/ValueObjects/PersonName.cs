using System.Collections.Generic;

namespace SmartHub.Domain.Entities.ValueObjects
{
	public class PersonName : ValueObject
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		protected PersonName()
		{

		}
		public PersonName(string first, string? middle, string last)
		{
			FirstName = first;
			MiddleName = middle ?? "";
			LastName = last;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return FirstName;
			yield return MiddleName;
			yield return LastName;
		}
	}
}
