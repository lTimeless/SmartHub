using System;

namespace SmartHub.Application.Common.Models
{
    public class ServerLog
    {
        public string? Id { get; }
        public string? DateTime { get; }
        public string? Level { get; }
        public string? Message { get; }
        public string? Exception { get; }

        public ServerLog(string? dateTime, string? level, string? message, string? exception)
        {
            Id = Guid.NewGuid().ToString();
            DateTime = dateTime;
            Level = level;
            Message = message;
            Exception = exception;
        }
    }
}