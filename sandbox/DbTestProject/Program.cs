using System;
using System.Collections.Generic;
using System.Linq;
using DbTestProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTestProject
{
    class Program
    {
        private static User GetRandomUser(List<User> users)
        {
            var rand = new Random();
            return users[rand.Next(0, users.Count)];
        }

        private static Song GetRandomSong(List<Song> songs)
        {
            var rand = new Random();
            return songs[rand.Next(0, songs.Count)];
        }

        private static Genre GetRandomGenre(IReadOnlyList<Genre> genres)
        {
            var rand = new Random();
            return genres[rand.Next(0, genres.Count)];
        }

        private static Quiz GetRandomQuiz(IReadOnlyList<Quiz> quizzes)
        {
            var rand = new Random();
            return quizzes[rand.Next(0, quizzes.Count)];
        }

        static void Main(string[] args)
        {
            GenerateData();
            GetData();
        }

        private static void GetData()
        {
            using var context = new TestDbContext();
            var users = context.Users.ToList();
            var genres = context.Genres.ToList();
            var quizzes = context.Quizzes.Include(q => q.QuizSongs).ThenInclude(qs => qs.Song).ToList();
            var songs = context.Songs.ToList();
            var scores = context.Scores.Include(s=>s.Quiz).ToList();

            Console.WriteLine("\nUsers:");
            users.ForEach(u => Console.WriteLine($"Username:{u.Username} Password:{u.Password} Rating:{u.AppRating}"));

            Console.WriteLine("\nGenres:");
            genres.ForEach(g => Console.WriteLine($"Name: {g.Name}"));

            Console.WriteLine("\nQuizzes:");
            quizzes.ForEach(q => Console.WriteLine($"Title:{q.Title} PlayTime:{q.PlayTime} AnswerTime:{q.AnswerTime} Id:{q.Id} Genre:{q.GenreId} Owner:{q.OwnerUserId}\n\tSongs:{SongsToString(q)}"));

            Console.WriteLine("\nSongs:");
            songs.ForEach(s => Console.WriteLine($"Link:{s.Link}"));


            Console.WriteLine("\nScores:");
            scores.ForEach(s => Console.WriteLine($"Id:{s.Id} User:{s.OwnerUserId} Quiz:{s.Quiz.Title} Points:{s.Points}"));
        }

        private static string SongsToString(Quiz quiz)
        {
            var result = string.Empty;
            quiz.QuizSongs.ForEach(qs => result += $"{qs.Song.Link} ");
            return result;
        }

        private static void GenerateData()
        {
            var rand = new Random();
            var dataGenerator = new DataGenerator();
            var users = dataGenerator.CreateUsers(10);
            var genres = dataGenerator.CreateGenres(10);
            var quizzes = dataGenerator.CreateQuizzes(20);
            var songs = dataGenerator.CreateSongs(50);
            var scores = dataGenerator.CreateScores(10);
            foreach (var quiz in quizzes)
            {
                quiz.OwnerUser = GetRandomUser(users.ToList());
                quiz.QuizSongs = new List<QuizSong>();
                var numOfSongs = rand.Next(1, 5);
                for (var i = 0; i < numOfSongs; i++)
                {
                    quiz.QuizSongs.Add(new QuizSong() { Song = GetRandomSong(songs.ToList()) });
                }
                quiz.Genre = GetRandomGenre(genres.ToList());
            }

            foreach (var score in scores)
            {
                score.OwnerUser = GetRandomUser(users.ToList());
                score.Quiz = GetRandomQuiz(quizzes.ToList());
            }

            using var context = new TestDbContext();
            context.Users.AddRange(users);
            context.Genres.AddRange(genres);
            context.Quizzes.AddRange(quizzes);
            context.Songs.AddRange(songs);
            context.Scores.AddRange(scores);
            context.SaveChanges();
        }
    }
}