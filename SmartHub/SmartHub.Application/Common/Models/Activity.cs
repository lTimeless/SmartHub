using System;

namespace SmartHub.Application.Common.Models
{
    /// <summary>
    /// It actually is the current request but with only relevant Infos
    /// </summary>
    public class Activity
    {
        public string Id { get; }
        public string DateTime { get; }
        public string Username { get; }
        public string Message { get; }
        public long ExecutionTime { get; }
        public bool? SuccessfulRequest { get; }

        public Activity(string dateTime, string userName, string message, long execTime, bool? successfullRequest = default)
        {
            Id = Guid.NewGuid().ToString();
            SuccessfulRequest = successfullRequest;
            (DateTime, Username, Message, ExecutionTime) = (dateTime, userName, message, execTime);
        }
    }
}