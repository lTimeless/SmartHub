using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.Domain.Entities.Devices
{
	public class Company : ValueObject
	{
		public string Name { get; set; }
		public string ShortName { get; set; }

		protected Company()
		{
		}

		public Company(string name)
		{
			Name = name;
			ShortName = name.Substring(0, 4);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Name;
			yield return ShortName;
		}
	}
}
