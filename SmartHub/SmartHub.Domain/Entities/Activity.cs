namespace SmartHub.Domain.Entities
{
    public class Activity : BaseEntity
    {
        public string DateTime { get; }
        public string Username { get; }
        public string Message { get; }
        public long ExecutionTime { get; }
        public bool? SuccessfulRequest { get; }

        public Activity(string dateTime, string userName, string message, long execTime, bool? successfullRequest = default)
        {
            SuccessfulRequest = successfullRequest;
            (DateTime, Username, Message, ExecutionTime) = (dateTime, userName, message, execTime);
        }
    }
}