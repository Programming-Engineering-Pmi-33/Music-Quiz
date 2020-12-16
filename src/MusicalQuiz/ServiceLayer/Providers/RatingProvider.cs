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

        public static async Task<double?> GetAverage()
        {
            await using var storage = new MusicalQuizDbContext();
            var rating = await storage.Users.Where(u => u.AppRating != null).Select(s => s.AppRating).ToListAsync();
            if (rating == null || rating.Count == 0)
            {
                return 0;
            }

            return rating.Average();
        }

        public static async Task<List<Score>> GetAllScores()
        {
            await using var storage = new MusicalQuizDbContext();
            var users = await storage.Scores.Include(s => s.OwnerUser).Select(s => s.OwnerUser).ToListAsync();
            return users.Select(u => new Score(u.Username, storage.Scores.Include(s => s.OwnerUser)
                    .Where(s => s.OwnerUser == u)
                    .Sum(s => s.Points)))
                .ToList();
        }

        public static async Task<List<Score>> GetScoreOfOneQuiz(int quizId)
        {
            await using var storage = new MusicalQuizDbContext();
            var users = await storage.Scores.Include(s => s.OwnerUser)
                .Where(s => s.QuizId == quizId)
                .Select(s => s.OwnerUser)
                .ToListAsync();

            return users.Select(u => new Score(u.Username, storage.Scores.Include(s => s.OwnerUser)
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

            await using var storage = new MusicalQuizDbContext();
            (await storage.Users.FirstOrDefaultAsync(u => u.Username == UsersProvider.CurrentUser.Username)).AppRating = points;
            await storage.SaveChangesAsync();
            await UsersProvider.ReloadUserData();
        }

        public static async Task AddResult(int quizId, int points)
        {
            await using var storage = new MusicalQuizDbContext();
            if (!UsersProvider.IsLoggedIn)
            {
                throw new UnauthorizedException();
            }

            if (await storage.Scores.FirstOrDefaultAsync(s =>
                s.OwnerUserId == UsersProvider.CurrentUser.Username && s.QuizId == quizId) == null)
            {
                await storage.Scores.AddAsync(new StorageLayer.Models.Score()
                { OwnerUserId = UsersProvider.CurrentUser.Username, QuizId = quizId, Points = points });
                await storage.SaveChangesAsync();
            }
        }
    }
}