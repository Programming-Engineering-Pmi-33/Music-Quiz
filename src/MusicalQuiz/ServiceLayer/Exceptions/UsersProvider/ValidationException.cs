namespace ServiceLayer.Exceptions.UsersProvider
{
    public class ValidationException : UsersProviderException
    {
        public ValidationException(string message) : base(message) { }
    }
}