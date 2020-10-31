using System.Collections.Generic;
using SmartHub.Domain.Common.Extensions;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public virtual List<Device> Devices { get; set; } = new List<Device>();

		protected Group()
		{
		}

		public Group(string name, string description) : base(name, description)
		{
		}

		#region Methods

		public Group AddDevice(Device newDevice)
		{
			Devices.Add(newDevice);
			return this;
		}
		#endregion

	}
}
