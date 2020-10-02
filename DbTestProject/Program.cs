using System;
using System.Collections.Generic;
using System.Linq;
using DbTestProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTestProject
{
    class Program
    {
        static User GenerateUser()
        {
            return new User()
            {
                Username = "superuser",
                Password = "kebab",
                AppRating = new AppRating()
                {
                    Points = 5
                },
                Quizzes = GenerateQuizzes(1),
                Scores = new List<Score>()
                {
                    new Score()
                    {
                        Points = 5,
                        QuizId = 1,
                    },
                    new Score()
                    {
                        Points = 4,
                        QuizId = 2,
                    }

                }
            };
        }

        static List<Quiz> GenerateQuizzes(int param)
        {
            return new List<Quiz>()
            {
                new Quiz()
                {
                    PlayTime = 10 + param,
                    Title = $"My quiz {param}",
                    AnswerTime = 5 + param,
                    QuizSongs = GenerateQuizSongs(param),
                    Genre = new Genre(){Name = "Rock"}
                },

                new Quiz()
                {
                    PlayTime = 15+param,
                    Title = $"My quiz {param+1}",
                    AnswerTime = 10+param,
                    QuizSongs = GenerateQuizSongs(param+1),
                    Genre = new Genre(){Name = "Rap"}
                }
            };
        }

        static List<QuizSong> GenerateQuizSongs(int param)
        {
            var quizzesSongs = new List<QuizSong>();
            for (var i = 0; i < 2; i++)
            {
                var quizSong = new QuizSong()
                {
                    Song = new Song()
                    {
                        Link = $"http://open.spotify.com/track/6rqhFgbbKwnb9MLmUQDhG{param}{i}"
                    }
                };
                quizzesSongs.Add(quizSong);
            }

            return quizzesSongs;
        }

        static void Main(string[] args)
        {
            using (var context = new TestDbContext())
            {

                context.Users.Add(GenerateUser());
                context.SaveChanges();

                var user = context.Users
                    .Include(u => u.Quizzes)
                    .Include(u => u.Scores)
                    .Include(u => u.AppRating)
                    .First();

                PrintData(user);
            }
        }

        public static void PrintData(User user)
        {
            Console.WriteLine($"User: {user.Username}, password: {user.Password}");
            Console.WriteLine($"App rating: {user.AppRating.Points}");
            Console.WriteLine("Quizzes:");
            foreach (var quiz in user.Quizzes)
            {
                Console.WriteLine($"\nQuiz: {quiz.Title}, genre: {quiz.Genre.Name}, answer time: {quiz.AnswerTime}, play time: {quiz.PlayTime}\n\tSongs:");
                foreach (var quizSong in quiz.QuizSongs)
                {
                    Console.WriteLine($"\tLink: {quizSong.Song.Link}");
                }
            }
            Console.WriteLine("Scores:");
            foreach (var score in user.Scores)
            {
                Console.WriteLine($"Name: {score.Quiz.Title}, points: {score.Points}");
            }
        }
    }
}