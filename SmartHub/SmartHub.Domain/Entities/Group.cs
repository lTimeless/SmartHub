using System.Collections.Generic;
using SmartHub.Domain.Entities.Devices;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public string? HomeId { get; private set; }
		public virtual Home Home { get; }
		public virtual List<Device> Devices { get; set; }

		protected Group()
		{
		}

		public Group(string name, string description, Home home) :
			base(name, description)
		{
			Home = home;
			Devices = new List<Device>();
		}
	}
}
