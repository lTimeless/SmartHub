using System.Collections.Generic;

namespace SmartHub.Domain.Entities
{
	public class Group : BaseEntity
	{
		public virtual List<Device> Devices { get; set; } = new();

		public Group()
		{
		}

		public Group(string name, string? description) : base(name, description)
		{
		}
	}
}
