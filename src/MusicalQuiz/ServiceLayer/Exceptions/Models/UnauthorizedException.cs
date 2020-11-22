using System;
using System.Runtime.Serialization;

namespace ServiceLayer.Exceptions.Models
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public UnauthorizedException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}