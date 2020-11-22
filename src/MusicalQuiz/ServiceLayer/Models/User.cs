namespace ServiceLayer.Models
{
    public class User
    {
        public string Username { get; }
        public string Password { get; }
        public int? AppRating { get; }

        public User(string username, string password, int? appRating)
        {
            Username = username;
            Password = password;
            AppRating = appRating;
        }
    }
}