using System;
using SmartHub.Application.Common.Mappings;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.SignalR
{
    /// <summary>
    /// It is the current request but only with relevant Infos
    /// </summary>
    public class ActivityDto : BaseDto, IMapFrom<Activity>
    {
        public string DateTime { get; }
        public string Username { get; }
        public string Message { get; }
        public long ExecutionTime { get; }
        public bool? SuccessfulRequest { get; }

        public ActivityDto(string dateTime, string userName, string message, long execTime, bool? successfullRequest = default)
        {
            Id = Guid.NewGuid().ToString();
            SuccessfulRequest = successfullRequest;
            (DateTime, Username, Message, ExecutionTime) = (dateTime, userName, message, execTime);
        }
    }
}