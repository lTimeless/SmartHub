namespace SmartHub.Domain.Entities
{
    public class Activity : BaseEntity
    {
        public string DateTime { get; } = null!;
        public string Username { get; } = null!;
        public string Message { get; } = null!;
        public long ExecutionTime { get; }
        public bool SuccessfulRequest { get; }

        protected Activity()
        {
        }
    }
}