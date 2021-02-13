using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Groups
{
	public class GroupPayload : Payload
	{
		public Group? Group { get; }

		public GroupPayload(Group? group, string? message = null) : base(message)
		{
			Group = group;
		}

		public GroupPayload(UserError error) : base(new []{ error })
		{
		}
	}
}