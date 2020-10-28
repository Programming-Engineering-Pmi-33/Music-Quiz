using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StorageLayer.Models;

namespace StorageLayer
{
    public class MusicalQuizDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Song> Songs { get; set; }

        public MusicalQuizDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetDbConnection());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<QuizSong>().HasKey(qs => new { qs.QuizId, qs.SongId });
            modelBuilder.Entity<Score>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Song>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Genre>().Property(g => g.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Quiz>().ToTable("quizzes");
            modelBuilder.Entity<Genre>().ToTable("genres");
            modelBuilder.Entity<Score>().ToTable("scores");
            modelBuilder.Entity<Song>().ToTable("songs");
            modelBuilder.Entity<QuizSong>().ToTable("quiz_songs");
        }

        private static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);


#if DEBUG
            var strConnection = builder.Build().GetConnectionString("DevelopmentDbConnection");
#endif

#if !DEBUG
            var strConnection = builder.Build().GetConnectionString("ProductionDbConnection");
#endif
            return strConnection;
        }
    }
}
