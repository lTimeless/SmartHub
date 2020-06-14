using System;
using SmartHub.Application.Common.Mappings;

namespace SmartHub.Application.UseCases
{
	public abstract class BaseDto
	{
		public string Id { get; private set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
