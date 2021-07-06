using System;

namespace ServiceLayer.Exceptions.UsersProvider
{
    public class UsersProviderException : Exception
    {
        protected UsersProviderException(string message) : base(message) { }
    }
}