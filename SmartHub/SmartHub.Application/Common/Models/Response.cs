using System.Collections.Generic;

namespace SmartHub.Application.Common.Models
{
    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default) =>
            new Response<T>(data, false, message);

        public static Response<T> Ok<T>(string message, T data) =>
            new Response<T>(data, true, message);
        public static Response<T> Ok<T>(T data) =>
            new Response<T>(data, true, null);
    }

    public class Response<T>
    {
        public T Data { get; }
        public bool? Success { get; }
        public string? Message { get; }


        public Response(T data,  bool success, string? message)
        {
            Data = data;
            Success = success;
            Message = message ;
        }
    }
}
