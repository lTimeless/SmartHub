using System.Collections.Generic;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public virtual List<Device> Devices { get; set; } = new();

		public bool IsSubGroup { get; set; }
		public virtual List<Group> SubGroups { get; set; } = new();

		public Group()
		{
		}

		public Group(string name, string? description, bool isSubGroup = default) : base(name, description)
		{
			IsSubGroup = isSubGroup;
		}

	}
}
