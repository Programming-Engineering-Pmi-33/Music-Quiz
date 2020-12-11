using System;
using System.Runtime.Serialization;

namespace ServiceLayer.Exceptions.UsersProvider
{
    public class UsersProviderException : Exception
    {
        public UsersProviderException()
        {
        }

        public UsersProviderException(string message) : base(message)
        {
        }

        protected UsersProviderException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public UsersProviderException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}