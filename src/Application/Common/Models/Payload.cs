using System.Collections.Generic;

namespace SmartHub.Application.Common.Models
{
	public abstract class Payload
	{
		public IReadOnlyList<UserError>? Errors { get; }
		// TODO brauch man MEssage???
		public string? Message { get; }

		protected Payload(IReadOnlyList<UserError>? errors = null)
		{
			Message = default;
			Errors = errors;
		}

		protected Payload(string? message = null)
		{
			Message = message;
			Errors = default;
		}
	}
}