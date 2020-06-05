namespace SmartHub.Domain.Common
{
    public class ServiceResponse<T> where T : class
    {
        public T? Data { get; }
        public bool? Success { get; }
        public string? Message { get; }

        public ServiceResponse(T? data = null, bool success = false, string? message = null)
        {
            Data = data;
            Success = success;
            Message = message;
        }

    }
}
