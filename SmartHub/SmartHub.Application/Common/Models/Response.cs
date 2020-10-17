
namespace SmartHub.Application.Common.Models
{
    /// <summary>
    /// A Class which represents the Reponse
    /// </summary>
    public static class Response
    {
        /// <summary>
        /// Creates a Response with a Fail preset
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="data">The data you want to send</param>
        /// <typeparam name="T">This can be any type</typeparam>
        /// <returns>A new Failed Response</returns>
        public static Response<T> Fail<T>(string message, T data = default) =>
            new Response<T>(data, false, message);

        /// <summary>
        /// Creates a Response with an Ok preset
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="data">The data you want to send</param>
        /// <typeparam name="T">This can be any type</typeparam>
        /// <returns>A new Ok Response</returns>
        public static Response<T> Ok<T>(string message, T data = default)
        {
            return new Response<T>(data, true, message);
        }

        /// <summary>
        /// Creates a Response with an Ok preset
        /// </summary>
        /// <param name="data">The data you want to send</param>
        /// <typeparam name="T">This can be any type</typeparam>
        /// <returns>A new Ok Response, without a message</returns>
        public static Response<T> Ok<T>(T data = default) =>
            new Response<T>(data, true, null);
    }

    public class Response<T>
    {
        public T Data { get; set; }
        public bool? Success { get; set; }
        public string? Message { get; set; }


        public Response(T data,  bool success, string? message)
        {
            Data = data;
            Success = success;
            Message = message ;
        }
    }
}
