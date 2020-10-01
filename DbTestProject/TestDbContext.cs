using System.IO;
using DbTestProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbTestProject
{
    public class TestDbContext : DbContext
    {
        public DbSet<TestModel> TestDbSet { get; set; }

        public TestDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetDbConnection());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestModel>().HasKey(k=> k.Id);
            modelBuilder.Entity<TestModel>().Property(p => p.Id).ValueGeneratedOnAdd();
        }

        private static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            var strConnection = builder.Build().GetConnectionString("DbConnection");

            return strConnection;
        }
    }
}
