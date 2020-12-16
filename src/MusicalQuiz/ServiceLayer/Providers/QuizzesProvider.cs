using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Exceptions.Models;
using StorageLayer;
using StorageLayer.Models;
using Quiz = ServiceLayer.Models.Quiz;

namespace ServiceLayer.Providers
{
    public static class QuizzesProvider
    {
        public static async Task Create(Quiz quiz)
        {
            if (quiz.Id != 0)
            {
                throw new QuizValidationException(nameof(quiz.Id));
            }

            await using var storage = new MusicalQuizDbContext();
            StorageLayer.Models.Quiz storageQuiz;
            if (await storage.Genres.FirstOrDefaultAsync(g => g.Name == quiz.Genre) == null)
            {
                storageQuiz = new StorageLayer.Models.Quiz
                {
                    OwnerUserId = quiz.OwnerId,
                    Title = quiz.Title,
                    PlayTime = quiz.PlayTime,
                    AnswerTime = quiz.AnswerTime,
                    Genre = new Genre { Name = quiz.Genre },
                    QuizSongs = new List<QuizSong>()
                };
            }
            else
            {
                storageQuiz = new StorageLayer.Models.Quiz
                {
                    OwnerUserId = quiz.OwnerId,
                    Title = quiz.Title,
                    PlayTime = quiz.PlayTime,
                    AnswerTime = quiz.AnswerTime,
                    GenreId = quiz.Genre,
                    QuizSongs = new List<QuizSong>()
                };
            }

            var existingSongs = quiz.Songs.Where(s => storage.Songs.FirstOrDefaultAsync(ss => ss.Link == s) != null).ToList();
            foreach (var link in existingSongs)
            {
                var song = await storage.Songs.FirstAsync(s => s.Link == link);
                storageQuiz.QuizSongs.Add(new QuizSong { SongId = song.Id });
            }

            var newSongs = quiz.Songs.Where(s => storage.Songs.FirstOrDefaultAsync(ss => ss.Link == s) == null).ToList();
            foreach (var link in newSongs)
            {
                storageQuiz.QuizSongs.Add(new QuizSong { Song = new Song { Link = link } });
            }

            await storage.Quizzes.AddAsync(storageQuiz);
        }

        public static async Task<List<Quiz>> GetAllQuizzes()
        {
            await using var storage = new MusicalQuizDbContext();
            var storageQuizzes = await storage.Quizzes.Include(q => q.QuizSongs)
                .ThenInclude(q => q.Song)
                .OrderByDescending(q => q.CreatedDateTime)
                .ToListAsync();

            return storageQuizzes.Select(s =>
                new Quiz(s.OwnerUserId,
                    s.Title,
                    s.PlayTime,
                    s.AnswerTime,
                    s.GenreId,
                    s.QuizSongs.Select(qs => qs.Song.Link),
                    s.Id))
                .ToList();
        }

        public static async Task<List<Quiz>> GetOwnersQuizzes()
        {
            await using var storage = new MusicalQuizDbContext();
            if (!UsersProvider.IsLoggedIn)
            {
                throw new UnauthorizedException();
            }
            var storageQuizzes = await storage.Quizzes.Include(q => q.QuizSongs)
                .ThenInclude(q => q.Song)
                .OrderByDescending(q => q.CreatedDateTime)
                .Where(q => q.OwnerUserId == UsersProvider.CurrentUser.Username)
                .ToListAsync();

            return storageQuizzes.Select(s =>
                new Quiz(s.OwnerUserId,
                    s.Title,
                    s.PlayTime,
                    s.AnswerTime,
                    s.GenreId,
                    s.QuizSongs.Select(qs => qs.Song.Link),
                    s.Id))
                .ToList();
        }

        public static async Task<Quiz> GetQuiz(int quizId)
        {
            await using var storage = new MusicalQuizDbContext();
            var storageQuiz = await storage.Quizzes.Include(q => q.QuizSongs)
                .ThenInclude(q => q.Song)
                .FirstOrDefaultAsync(q => q.Id == quizId);
            return new Quiz(storageQuiz.OwnerUserId,
                storageQuiz.Title,
                storageQuiz.PlayTime,
                storageQuiz.AnswerTime,
                storageQuiz.GenreId,
                storageQuiz.QuizSongs.Select(s => s.Song.Link));
        }
    }
}