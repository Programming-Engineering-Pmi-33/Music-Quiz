using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                Username = $"superuser",
                Password = $"kebab",
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

        static List<Quiz> GenerateQuizzes(int i)
        {
            return new List<Quiz>()
            {
                new Quiz()
                {
                    PlayTime = 10 + i,
                    Title = $"My quiz {i}",
                    AnswerTime = 5 + i,
                    QuizSongs = GenerateQuizSongs(i),
                    Genre = new Genre(){Name = "Rock"}
                },

                new Quiz()
                {
                    PlayTime = 15+i,
                    Title = $"My quiz {i+1}",
                    AnswerTime = 10+i,
                    QuizSongs = GenerateQuizSongs(i+1),
                    Genre = new Genre(){Name = "Rap"}
                }
            };
        }

        static List<QuizSong> GenerateQuizSongs(int q)
        {
            var quizzesSongs = new List<QuizSong>();
            for (int i = 0; i < 2; i++)
            {
                var quizSong = new QuizSong()
                {
                    Song = new Song()
                    {
                        Link = $"http://open.spotify.com/track/6rqhFgbbKwnb9MLmUQDhG{q}{i}"
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
            foreach (var i in user.Quizzes)
            {
                Console.WriteLine($"\nQuiz: {i.Title}, genre: {i.Genre.Name}, answer time: {i.AnswerTime}, play time: {i.PlayTime}\n\tSongs:");
                foreach (var j in i.QuizSongs)
                {
                    Console.WriteLine($"\tLink: {j.Song.Link}");
                }
            }
            Console.WriteLine("Scores:");
            foreach (var i in user.Scores)
            {
                Console.WriteLine($"Name: {i.Quiz.Title}, points: {i.Points}");
            }
        }
    }
}