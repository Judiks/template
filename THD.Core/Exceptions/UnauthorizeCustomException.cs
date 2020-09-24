using System;

namespace THD.Core.Exceptions
{
    public class UnauthorizeCustomException : Exception
    {
        public UnauthorizeCustomException(string message) : base(message)
        {

        }
    }
}
