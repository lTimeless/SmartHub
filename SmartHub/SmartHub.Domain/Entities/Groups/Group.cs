using SmartHub.Domain.Entities.Devices;
using SmartHub.Domain.Entities.Homes;
using System.Collections.Generic;

namespace SmartHub.Domain.Entities.Groups
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
