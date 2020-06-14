using System.Collections.Generic;

namespace SmartHub.Application.Common.Models
{
    public class ServiceResponse<T> where T : class
    {
        public T? Data { get; }
        public bool? Success { get; }
        public string? Message { get; }

        public IEnumerable<string>? Errors { get; }

        public ServiceResponse(T? data, IEnumerable<string>? errors,  bool success, string? message)
        {
            Errors = errors ?? null;
            Data = data ?? null;
            Success = success;
            Message = message ?? null;
        }

        public ServiceResponse(T data, bool success, string? message)
        {
            Success = success;
            Data = data ?? null;
            Message = message ?? null;
            Errors = null;
        }

        public ServiceResponse(bool success, string message)
        {
            Success = success;
            Data = null;
            Message = message ;
            Errors = null;
        }

        public ServiceResponse(T data, bool success)
        {
            Data = data;
            Success = success;
            Message = null;
            Errors = null;
        }

    }
}
