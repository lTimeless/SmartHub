using System;

namespace SmartHub.Application.UseCases
{
	/// <summary>
	/// The base DTO from which DTO can inherit from if it has the same props.
	/// </summary>
	public abstract class BaseDto
	{
		public string? Id { get;  set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset LastModifiedAt { get; set; }
		public string? CreatedBy { get; set; }
		public string? LastModifiedBy { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
	}
}
