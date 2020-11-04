namespace SmartHub.Domain.Entities
{
    public class Activity : BaseEntity
    {
        // TODO: make this class to a dotnet5 record
        public string DateTime { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public long ExecutionTime { get; set; }
        public bool SuccessfulRequest { get; set; }

        // Needed for ef core
        protected Activity()
        {
        }
    }
}