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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetDbConnection());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizSong>().HasKey(qs => new { qs.QuizId, qs.SongId });
            modelBuilder.Entity<Score>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Song>().Property(u => u.Id).ValueGeneratedOnAdd();
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
