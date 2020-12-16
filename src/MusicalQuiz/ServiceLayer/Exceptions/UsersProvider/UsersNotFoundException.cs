namespace ServiceLayer.Exceptions.UsersProvider
{
    public class UsersNotFoundException : UsersProviderException
    {
        public UsersNotFoundException(string message) : base(message) { }
    }
}