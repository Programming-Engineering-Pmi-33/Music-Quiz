﻿using System;
using System.Linq;
using System.Collections.Generic;
 using DbTestProject.Models;
 using EmbedIO;
 using Microsoft.EntityFrameworkCore.Query.Internal;


 namespace DbTestProject
{
    public class DataGenerator
    {
        public string GenerateRandomText()
        {
            char[] lowers = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
            char[] uppers = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
            char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
 
            var l = lowers.Length;
            var u = uppers.Length;
            var n = numbers.Length;
 
            var rnd = new Random();

            var randomText = "";
 
            randomText += lowers[rnd.Next(0, l)].ToString();
            randomText += lowers[rnd.Next(0, l)].ToString();
            randomText += lowers[rnd.Next(0, l)].ToString();
 
            randomText += uppers[rnd.Next(0, u)].ToString();
            randomText += uppers[rnd.Next(0, u)].ToString();
            randomText += uppers[rnd.Next(0, u)].ToString();
 
            randomText += numbers[rnd.Next(0, n)].ToString();
            randomText += numbers[rnd.Next(0, n)].ToString();
            randomText += numbers[rnd.Next(0, n)].ToString();
 
            return randomText;
        }

        public List<string> GenerateUsernames(int n)
        {
            HashSet<string> hset = new HashSet<string>();
            while (hset.Count < n) {
                hset.Add(GenerateRandomText());
            }
            List<string> usernames = hset.ToList();
            return usernames;
        }

        public List<string> GenerateSongIds(int n)
        {
            HashSet<string> hset = new HashSet<string>();
            while (hset.Count < n) {
                hset.Add(GenerateRandomText());
            }
            List<string> ids = hset.ToList();
            return ids;
        }

        public List<string> GeneratePasswords(int n)
        {
            List<string> passwords = new List<string>();
            for (int i = 0; i < n; ++i)
            {
                passwords.Add(GenerateRandomText());
            }
            return passwords;
        }

        public IEnumerable<User> CreateUsers(int quantity)
        {
            var users = new List<User>(quantity);
            for (var u = 0; u < quantity; u++)
            {
                users.Add(new User());
            }

            var usernames = GenerateUsernames(quantity);
            var passwords = GeneratePasswords(quantity);
            var rand = new Random();
            for (var u = 0; u < quantity; u++)
            {
                users[u].Username = usernames[u];
                users[u].Password = passwords[u];
                users[u].AppRating = rand.Next(1, 5);
            }

            return users;
        }

        public IEnumerable<Genre> CreateGenres(int quantity)
        {
            var genres = new List<Genre>();
            for (var g = 0; g < quantity; g++)
            {
                genres.Add(new Genre(){Name = GenerateRandomText()});
            }

            return genres;
        }


        public IEnumerable<Quiz> CreateQuizzes(int quantity)
        {
            var random = new Random();
            var quizzes = new List<Quiz>();
            for (var q = 0; q < quantity; q++)
            {
                quizzes.Add(new Quiz()
                {
                    AnswerTime = random.Next(10, int.MaxValue),
                    PlayTime = random.Next(2, 25),
                    Genre = new Genre(){Name = GenerateRandomText()},
                    Title = GenerateRandomText()
                });
            }

            return quizzes;
        }

        public IEnumerable<Song> CreateSongs(int quantity)
        {
            var songs = new List<Song>();
            for (var s = 0; s < quantity; s++)
            {
                songs.Add(new Song(){Link = $"https:/api.spotify.track/{GenerateRandomText()}"});
            }

            return songs;
        }

        public IEnumerable<Score> CreateScores(int quantity)
        {
            var rand = new Random();
            var scores = new List<Score>();
            for (var s = 0; s < quantity; s++)
            {
                scores.Add(new Score() {Points = rand.Next(0, 20)});
            }

            return scores;
        }
    }
}