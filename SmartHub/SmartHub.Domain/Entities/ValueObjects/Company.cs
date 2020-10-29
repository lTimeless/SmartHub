﻿using System.Collections.Generic;

namespace SmartHub.Domain.Entities.ValueObjects
{
	public class Company : ValueObject
	{
		public string Name { get; } = default!;
		public string ShortName { get; } = default!;

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
