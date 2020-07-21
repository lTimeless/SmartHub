using System;
using NodaTime;

namespace SmartHub.Application.UseCases
{
	public abstract class BaseDto
	{
		public string Id { get;  set; }
		public Instant CreatedAt { get; set; }
		public Instant LastModifiedAt { get; set; }
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
