using System;

namespace Application.Common.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(int statusCode, string message = "", string details = "") : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }

        public int StatusCode { get; }

        public string Details { get; }

        public bool HasMessage => !string.IsNullOrEmpty(Message);

        public bool HasDetails => !string.IsNullOrEmpty(Details);
    }
}
