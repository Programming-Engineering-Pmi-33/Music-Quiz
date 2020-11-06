using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StorageLayer;
using StorageLayer.Models;

namespace ClientLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            LoadData();
            InitializeComponent();
        }

        public List<User> Users { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Song> Songs { get; set; }
        public List<Score> Scores { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddDataOnClick(object sender, RoutedEventArgs e)
        {
            using var context = new MusicalQuizDbContext();

            var userDmytro = new User()
            {
                Username = $"User{context.Users.Count() + 1}",
                Password = $"PaswordHash{context.Users.Count() + 1}",
                AppRating = context.Users.Count() % 6,
                Quizzes = new List<Quiz>()
            };
            context.Users.Add(userDmytro);


            context.Genres.AddRange(
                new Genre()
                { Name = $"Rock{context.Users.Count() + 1}" },
                new Genre()
                { Name = $"Rap{context.Users.Count() + 1}" });
            context.SaveChanges();

            var quiz = new Quiz()
            {
                Title = $"Rock generation {context.Users.Count() + 1}",
                Genre = context.Genres.FirstOrDefault(g => g.Name.Equals($"Rock{context.Users.Count()}")),
                AnswerTime = context.Users.Count() + 1,
                PlayTime = context.Users.Count() + 1 % 26 <= 2 ? 2 : context.Users.Count() + 1 % 26,
                QuizSongs = new List<QuizSong>()
                {
                    new QuizSong
                    {
                        Song = new Song()
                        {
                            Link = $"https:/spotify.web.api/some/track/{context.Users.Count()}"
                        }
                    }
                }
            };
            context.Quizzes.Add(quiz);
            userDmytro.Quizzes.Add(quiz);
            context.SaveChanges();

            userDmytro.Scores.Add(new Score() { Quiz = userDmytro.Quizzes.First(), Points = context.Users.Count() });
            context.SaveChanges();
            LoadData();
        }

        public void LoadData()
        {
            using var context = new MusicalQuizDbContext();
            Users = context.Users.Include(u => u.Quizzes).Include(u => u.Scores).ToList();
            Quizzes = context.Quizzes.Include(q => q.QuizSongs).Include(q => q.Genre).Include(q => q.OwnerUser)
                .ToList();
            Genres = context.Genres.ToList();
            Songs = context.Songs.Include(s => s.QuizSong).ToList();
            Scores = context.Scores.Include(s => s.Quiz).Include(s=>s.OwnerUser).ToList();
            OnPropertyChanged(nameof(Users));
            OnPropertyChanged(nameof(Quizzes));
            OnPropertyChanged(nameof(Genres));
            OnPropertyChanged(nameof(Songs));
            OnPropertyChanged(nameof(Scores));
        }

        private void CleanDataOnClick(object sender, RoutedEventArgs e)
        {
            using var context = new MusicalQuizDbContext();
            context.Users.RemoveRange(context.Users.ToList());
            context.Genres.RemoveRange(context.Genres.ToList());
            context.Songs.RemoveRange(context.Songs.ToList());
            context.SaveChanges();
            LoadData();
        }
    }
}
