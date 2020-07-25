using System.Collections.Generic;

namespace SmartHub.Application.Common.Models
{
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
        public static Response<T> Ok<T>(string message, T data) =>
            new Response<T>(data, true, message);
        /// <summary>
        /// Creates a Response with an Ok preset
        /// </summary>
        /// <param name="data">The data you want to send</param>
        /// <typeparam name="T">This can be any type</typeparam>
        /// <returns>A new Ok Response, without a message</returns>
        public static Response<T> Ok<T>(T data) =>
            new Response<T>(data, true, null);
    }

    public class Response<T>
    {
        private T Data { get; }
        private bool? Success { get; }
        private string? Message { get; }


        public Response(T data,  bool success, string? message)
        {
            Data = data;
            Success = success;
            Message = message ;
        }
    }
}
