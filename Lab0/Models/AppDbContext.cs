using Microsoft.EntityFrameworkCore;
using Lab0.Models;
using System.Text.Json;

namespace Lab0.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<AlbumModel> Albums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure conversions BEFORE seeding so EF knows how to store the values
            modelBuilder.Entity<AlbumModel>()
                .Property(a => a.Songs)
                .HasConversion(
                    v => string.Join(";", v ?? new List<string>()),
                    v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // DateOnly -> string and back
            modelBuilder.Entity<AlbumModel>()
                .Property(a => a.ReleaseDate)
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd"),
                    v => DateOnly.Parse(v)
                );

            // TimeSpan -> string and back
            modelBuilder.Entity<AlbumModel>()
                .Property(a => a.TotalDuration)
                .HasConversion(
                    v => v.ToString(),
                    v => TimeSpan.Parse(v)
                );

            // Now seed data (after conversions are configured)
            modelBuilder.Entity<Contact>()
                .HasData(
                    new Contact()
                    {
                        Id = 1,
                        Email = "adam@wsei.edu.pl",
                        Name = "Adam"
                    },
                    new Contact()
                    {
                        Id = 2,
                        Email = "ewa@wsei.edu.pl",
                        Name = "Ewa"
                    },
                    new Contact()
                    {
                        Id = 3,
                        Email = "karol@op.pl",
                        Name = "Karol"
                    }
                );

            modelBuilder.Entity<Organization>()
                .HasData(
                    new Organization()
                    {
                        Id = 101,
                        Name = "WSEI",
                        Address = "Św. Filipa 17, Kraków"
                    },
                    new Organization()
                    {
                        Id = 102,
                        Name = "PKP",
                        Address = "Polska"
                    }
                );

            modelBuilder.Entity<AlbumModel>()
                .HasData(
                    new AlbumModel()
                    {
                        Id = 1,
                        Name = "Hybrid Theory",
                        Band = "Linkin Park",
                        // For seeding EF will use the conversions above to store Songs, DateOnly and TimeSpan
                        Songs = new List<string> { "In the End", "Crawling" },
                        ChartPosition = 2,
                        ReleaseDate = new DateOnly(2000, 10, 24),
                        TotalDuration = new TimeSpan(0, 37, 45)
                    },
                    new AlbumModel()
                    {
                        Id = 2,
                        Name = "Nevermind",
                        Band = "Nirvana",
                        Songs = new List<string> { "Smells Like Teen Spirit", "Come As You Are" },
                        ChartPosition = 1,
                        ReleaseDate = new DateOnly(1991, 9, 24),
                        TotalDuration = new TimeSpan(0, 49, 23)
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
