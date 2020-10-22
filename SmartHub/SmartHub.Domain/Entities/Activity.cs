namespace SmartHub.Domain.Entities
{
    public class Activity : BaseEntity
    {
        public string DateTime { get; protected set; }
        public string Username { get; protected set; }
        public string Message { get; protected set; }
        public long ExecutionTime { get; protected set; }
        public bool SuccessfulRequest { get; protected set; }

        protected Activity()
        {
        }
        public Activity(string dateTime, string userName, string message, long execTime, bool successfulRequest = default)
        {
            SuccessfulRequest = successfulRequest;
            (DateTime, Username, Message, ExecutionTime) = (dateTime, userName, message, execTime);
        }

        public Activity UpdateName(string name)
        {
            Name = name;
            return this;
        }
    }
}