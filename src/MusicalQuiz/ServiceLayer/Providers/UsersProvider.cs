using System;
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

        public static async Task Login(string username, string password)
        {
            await using var storage = new MusicalQuizDbContext();
            var user = await storage.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase));
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
            await using var storage = new MusicalQuizDbContext();
            if (password.Length < 4)
            {
                throw new ValidationException(nameof(password));
            }

            if (username.Length < 4 || username.Length > 16)
            {
                throw new ValidationException(nameof(username));
            }

            if (await storage.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                throw new WrongUsersDataException("User already exists.");
            }

            var hashedPassword = PasswordHasher.HashPassword(password);
            var storageUser = new StorageLayer.Models.User { Username = username, Password = hashedPassword };
            await storage.Users.AddAsync(storageUser);
            await storage.SaveChangesAsync();
            await Login(username, password);
        }

        public static void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }

        public static async Task ReloadUserData()
        {
            await using var storage = new MusicalQuizDbContext();
            var storageUser = await storage.Users.FirstOrDefaultAsync(u => u.Username == CurrentUser.Username);
            CurrentUser = ConvertStorageUserToModel(storageUser);
        }

        public static async Task<bool> DoesUserExists(string username)
        {
            await using var storage = new MusicalQuizDbContext();
            return await storage.Users.FirstOrDefaultAsync(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase)) != null;
        }

        private static User ConvertStorageUserToModel(StorageLayer.Models.User storageUser) => new User(storageUser.Username, storageUser.Password, storageUser.AppRating);
    }
}