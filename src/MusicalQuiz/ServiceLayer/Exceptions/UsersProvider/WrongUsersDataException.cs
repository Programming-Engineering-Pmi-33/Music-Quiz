using System;
using System.Runtime.Serialization;

namespace ServiceLayer.Exceptions.UsersProvider
{
    public class WrongUsersDataException : UsersProviderException
    {
        public WrongUsersDataException()
        {
        }

        public WrongUsersDataException(string message) : base(message)
        {
        }

        protected WrongUsersDataException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public WrongUsersDataException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}