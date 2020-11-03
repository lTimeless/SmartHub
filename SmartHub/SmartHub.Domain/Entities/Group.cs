using System.Collections.Generic;
using SmartHub.Domain.Common.Extensions;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public virtual List<Device> Devices { get; set; } = new List<Device>();

		public bool IsSubGroup { get; set; }
		public virtual List<Group> SubGroups { get; set; } = new List<Group>();

		public Group()
		{
		}

		public Group(string name, string description, bool isSubGroup = default) : base(name, description)
		{
			IsSubGroup = isSubGroup;
		}

		#region Methods

		public Group AddDevice(Device newDevice)
		{
			Devices.Add(newDevice);
			return this;
		}

		public Group AddSubGroup(Group newSubGroup)
		{
			SubGroups.Add(newSubGroup);
			return this;
		}
		#endregion

	}
}
