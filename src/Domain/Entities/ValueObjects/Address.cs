using System.Collections.Generic;

namespace SmartHub.Domain.Entities.ValueObjects
{
	public class Address : ValueObject
	{
		public string Street { get; set; } = default!;
		public string City { get; set; } = default!;
		public string State { get; set; } = default!;
		public string Country { get; set; } = default!;
		public string ZipCode { get; set; } = default!;

		public Address()
		{
		}

		public Address(string street, string city, string state, string country, string zipcode)
		{
			Street = street;
			City = city;
			State = state;
			Country = country;
			ZipCode = zipcode;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			// Using a yield return statement to return each element one at a time
			yield return Street;
			yield return City;
			yield return State;
			yield return Country;
			yield return ZipCode;
		}
	}
}
