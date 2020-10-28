using System.Collections.Generic;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public virtual List<Device> Devices { get; }

		protected Group()
		{

		}
		public Group(string name, string description) : base(name, description)
		{
			Devices = new List<Device>();
		}

		#region Methods
		public Group SetName(string name)
		{
			Name = name;
			return this;
		}

		public Group SetDescription(string description)
		{
			Description = description;
			return this;
		}

		public Group AddDevice(Device newDevice)
		{
			Devices.Add(newDevice);
			return this;
		}
		#endregion

	}
}
