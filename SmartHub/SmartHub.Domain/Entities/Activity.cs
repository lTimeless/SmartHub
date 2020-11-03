namespace SmartHub.Domain.Entities
{
    public class Activity : BaseEntity
    {
        // TODO: make this class to a dotnet5 record
        public string DateTime { get; } = null!;
        public string Username { get; } = null!;
        public string Message { get; } = null!;
        public long ExecutionTime { get; }
        public bool SuccessfulRequest { get; }

        public Activity()
        {
        }
    }
}