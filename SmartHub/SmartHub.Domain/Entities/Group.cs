using System.Collections.Generic;
using SmartHub.Domain.Entities.Devices;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public string? HomeId { get; protected set; }
		public virtual Home Home { get; protected set; }
		public virtual ICollection<Device> Devices { get; protected set; }

		public string CreatorName { get; set; }

		protected Group()
		{
		}

		public Group(string name, string description, Home home, List<Device> devices, string groupCreator) :
			base(name, description)
		{
			Home = home;
			Devices = devices;
			CreatorName = groupCreator;
		}
	}
}
