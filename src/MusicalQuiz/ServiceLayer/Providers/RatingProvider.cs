using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Exceptions.Models;
using ServiceLayer.Models;
using StorageLayer;

namespace ServiceLayer.Providers
{
    public static class RatingProvider
    {
        private static MusicalQuizDbContext Storage { get; } = new MusicalQuizDbContext();

        public static async Task<double?> GetAverage()
        {
            var rating = await Storage.Users.Where(u => u.AppRating != null).Select(s => s.AppRating).ToListAsync();
            if (rating == null || rating.Count == 0)
            {
                return 0;
            }

            return rating.Average();
        }

        public static async Task<List<Score>> GetAllScores()
        {
            var users = await Storage.Scores.Include(s => s.OwnerUser).Select(s => s.OwnerUser).ToListAsync();
            return users.Select(u => new Score(u.Username, Storage.Scores.Include(s => s.OwnerUser)
                    .Where(s => s.OwnerUser == u)
                    .Sum(s => s.Points)))
                .ToList();
        }

        public static async Task<List<Score>> GetScoreOfOneQuiz(int quizId)
        {
            var users = await Storage.Scores.Include(s => s.OwnerUser)
                .Where(s => s.QuizId == quizId)
                .Select(s => s.OwnerUser)
                .ToListAsync();

            return users.Select(u => new Score(u.Username, Storage.Scores.Include(s => s.OwnerUser)
                    .Where(s => s.OwnerUser == u && s.QuizId == quizId)
                    .Sum(s => s.Points)))
                .OrderByDescending(s => s.Points)
                .ToList();
        }

        public static async Task RateProgram(int points)
        {
            if (!UsersProvider.IsLoggedIn)
            {
                throw new UnauthorizedException();
            }

            var user = await Storage.Users.FirstOrDefaultAsync(u => u.Username == UsersProvider.CurrentUser.Username);
            user.AppRating = points;
            Storage.Users.Update(user);
            await Storage.SaveChangesAsync();
        }

        public static async Task AddResult(int quizId, int points)
        {
            if (!UsersProvider.IsLoggedIn)
            {
                throw new UnauthorizedException();
            }

            if (await Storage.Scores.FirstOrDefaultAsync(s =>
                s.OwnerUserId == UsersProvider.CurrentUser.Username && s.QuizId == quizId) == null)
            {
                await Storage.Scores.AddAsync(new StorageLayer.Models.Score()
                { OwnerUserId = UsersProvider.CurrentUser.Username, QuizId = quizId, Points = points });
                await Storage.SaveChangesAsync();
            }
        }
    }
}