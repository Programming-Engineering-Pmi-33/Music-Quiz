namespace ServiceLayer.Exceptions.UsersProvider
{
    public class WrongUsersDataException : UsersProviderException
    {
        public WrongUsersDataException(string message) : base(message) { }
    }
}