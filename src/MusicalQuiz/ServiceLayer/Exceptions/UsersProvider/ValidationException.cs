using System;
using System.Runtime.Serialization;

namespace ServiceLayer.Exceptions.UsersProvider
{
    public class ValidationException : UsersProviderException
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}