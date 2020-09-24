using System;
using System.Collections.Generic;

namespace THD.Core.Exceptions
{
    public class ApplicationCustomException : Exception
    {
        public int? StatusCode { get; set; }
        public ApplicationCustomException(string message) : base(message)
        {
        }

        public ApplicationCustomException(List<string> messages) : base(string.Join(", ", messages.ToArray()))
        {
        }
        public ApplicationCustomException(int? statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
