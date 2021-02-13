using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Activities
{
	/// <summary>
	/// It is the current request but only with relevant Infos
	/// </summary>
	public class ActivityDto : BaseDto, IMapFrom<Activity>
	{
		public string? DateTime { get; set; }
		public string? Username { get; set; }
		public string? Message { get; set; }
		public long? ExecutionTime { get; set; }
		public bool? SuccessfulRequest { get; set; }

		public ActivityDto(string? dateTime, string? username, string? message, long? executionTime, bool? successfulRequest, string? name)
		{
			DateTime = dateTime;
			Username = username;
			Message = message;
			ExecutionTime = executionTime;
			SuccessfulRequest = successfulRequest;
			Name = name;
		}

		public ActivityDto()
		{
		}
	}
}