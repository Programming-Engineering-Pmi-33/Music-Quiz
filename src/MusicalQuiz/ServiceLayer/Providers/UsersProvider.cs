using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Exceptions.UsersProvider;
using ServiceLayer.Models;
using StorageLayer;

namespace ServiceLayer.Providers
{
    public static class UsersProvider
    {
        public static User CurrentUser { get; private set; }
        public static bool IsLoggedIn { get; private set; }

        private static MusicalQuizDbContext Storage { get; } = new MusicalQuizDbContext();

        public static async Task Login(string username, string password)
        {
            var user = await Storage.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new UsersNotFoundException(username);
            }

            CurrentUser = PasswordHasher.IsPasswordValid(password, user.Password)
                ? ConvertStorageUserToModel(user)
                : throw new WrongUsersDataException("password");
            IsLoggedIn = true;
        }

        public static async Task Register(string username, string password)
        {
            if (password.Length < 8)
            {
                throw new ValidationException(nameof(password));
            }

            if (username.Length < 2 || username.Length > 50)
            {
                throw new ValidationException(nameof(username));
            }

            var hashedPassword = PasswordHasher.HashPassword(password);
            var storageUser = new StorageLayer.Models.User() { Username = username, Password = hashedPassword };
            await Storage.Users.AddAsync(storageUser);
            await Storage.SaveChangesAsync();
            await Login(username, password);
        }

        public static void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }

        public static async Task<bool> DoesUserExists(string username) => await Storage.Users.FirstOrDefaultAsync(u => u.Username == username) != null;

        private static User ConvertStorageUserToModel(StorageLayer.Models.User storageUser) => new User(storageUser.Username, storageUser.Password, storageUser.AppRating);
    }
}