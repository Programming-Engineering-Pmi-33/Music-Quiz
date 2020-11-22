using System;
using System.Runtime.Serialization;

namespace ServiceLayer.Exceptions.UsersProvider
{
    public class UsersNotFoundException : UsersProviderException
    {
        public UsersNotFoundException()
        {
        }

        public UsersNotFoundException(string message) : base(message)
        {
        }

        protected UsersNotFoundException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public UsersNotFoundException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}