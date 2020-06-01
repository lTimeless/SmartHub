using SmartHub.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.Domain.Entities.Devices
{
	public class IpAddress : ValueObject
	{
		public string Ipv4 { get; set; }

		protected IpAddress()
		{
		}

		public IpAddress(string value)
		{
			Ipv4 = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Ipv4;
		}
	}
}
