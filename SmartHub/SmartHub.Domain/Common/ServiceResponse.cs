namespace SmartHub.Domain.Common
{
    public class ServiceResponse<T> where T : class
    {
        private T? Data { get; }
        private bool? Success { get; }
        private string? Message { get; }

        public ServiceResponse(T? data = null, bool success = false, string? message = null)
        {
            Data = data;
            Success = success;
            Message = message;
        }

    }
}
