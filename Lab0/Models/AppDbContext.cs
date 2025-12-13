using System;
using System.Collections.Generic;
using System.Linq;
using Lab0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lab0.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<AlbumModel> Albums { get; set; }

        // New: labels
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Album conversions
            modelBuilder.Entity<AlbumModel>()
                .Property(a => a.Songs)
                .HasConversion(
                    v => string.Join(";", v ?? new List<string>()),
                    v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata.SetValueComparer(
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));

            modelBuilder.Entity<AlbumModel>()
                .Property(a => a.ReleaseDate)
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd"),
                    v => DateOnly.Parse(v));

            modelBuilder.Entity<AlbumModel>()
                .Property(a => a.TotalDuration)
                .HasConversion(
                    v => v.ToString(),
                    v => TimeSpan.Parse(v));

            // Contact–Organization relationship
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Organization)
                .WithMany(o => o.Contacts)
                .HasForeignKey(c => c.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // New: Album–Label relationship (like Contact–Organization)
            modelBuilder.Entity<AlbumModel>()
                .HasOne(a => a.Label)
                .WithMany(l => l.Albums)
                .HasForeignKey(a => a.LabelId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed Organizations
            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 101,
                    Name = "WSEI",
                    Address = "Św. Filipa 17, Kraków"
                },
                new Organization
                {
                    Id = 102,
                    Name = "PKP",
                    Address = "Polska"
                });

            // Seed Contacts
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    Name = "Adam",
                    Email = "adam@wsei.edu.pl",
                    OrganizationId = 101
                },
                new Contact
                {
                    Id = 2,
                    Name = "Ewa",
                    Email = "ewa@wsei.edu.pl",
                    OrganizationId = 101
                },
                new Contact
                {
                    Id = 3,
                    Name = "Karol",
                    Email = "karol@op.pl",
                    OrganizationId = 102
                });

            // New: seed Labels (principal in relation)
            modelBuilder.Entity<Label>().HasData(
                new Label { Id = 1, Name = "Warner Bros. Records", Country = "USA" },
                new Label { Id = 2, Name = "DGC Records", Country = "USA" },
                new Label { Id = 3, Name = "EMI", Country = "UK" },
                new Label { Id = 4, Name = "Sony Music", Country = "Japan" }
            );

            // Seed Albums (with LabelId, >10 total)
            modelBuilder.Entity<AlbumModel>().HasData(
                new AlbumModel
                {
                    Id = 1,
                    Name = "Hybrid Theory",
                    Band = "Linkin Park",
                    Songs = new List<string> { "In the End", "Crawling" },
                    ChartPosition = 2,
                    ReleaseDate = new DateOnly(2000, 10, 24),
                    TotalDuration = new TimeSpan(0, 37, 45),
                    LabelId = 1
                },
                new AlbumModel
                {
                    Id = 2,
                    Name = "Nevermind",
                    Band = "Nirvana",
                    Songs = new List<string> { "Smells Like Teen Spirit", "Come As You Are" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1991, 9, 24),
                    TotalDuration = new TimeSpan(0, 49, 23),
                    LabelId = 2
                },
                new AlbumModel
                {
                    Id = 3,
                    Name = "Meteora",
                    Band = "Linkin Park",
                    Songs = new List<string> { "Numb", "Somewhere I Belong" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(2003, 3, 25),
                    TotalDuration = new TimeSpan(0, 36, 35),
                    LabelId = 1
                },
                new AlbumModel
                {
                    Id = 4,
                    Name = "Back in Black",
                    Band = "AC/DC",
                    Songs = new List<string> { "Hells Bells", "Back in Black" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1980, 7, 25),
                    TotalDuration = new TimeSpan(0, 42, 11),
                    LabelId = 3
                },
                new AlbumModel
                {
                    Id = 5,
                    Name = "OK Computer",
                    Band = "Radiohead",
                    Songs = new List<string> { "Paranoid Android", "Karma Police" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1997, 5, 21),
                    TotalDuration = new TimeSpan(0, 53, 27),
                    LabelId = 3
                },
                new AlbumModel
                {
                    Id = 6,
                    Name = "Californication",
                    Band = "Red Hot Chili Peppers",
                    Songs = new List<string> { "Scar Tissue", "Otherside" },
                    ChartPosition = 3,
                    ReleaseDate = new DateOnly(1999, 6, 8),
                    TotalDuration = new TimeSpan(0, 56, 24),
                    LabelId = 4
                },
                new AlbumModel
                {
                    Id = 7,
                    Name = "The Dark Side of the Moon",
                    Band = "Pink Floyd",
                    Songs = new List<string> { "Time", "Money" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1973, 3, 1),
                    TotalDuration = new TimeSpan(0, 42, 49),
                    LabelId = 3
                },
                new AlbumModel
                {
                    Id = 8,
                    Name = "Abbey Road",
                    Band = "The Beatles",
                    Songs = new List<string> { "Come Together", "Something" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1969, 9, 26),
                    TotalDuration = new TimeSpan(0, 47, 23),
                    LabelId = 3
                },
                new AlbumModel
                {
                    Id = 9,
                    Name = "Thriller",
                    Band = "Michael Jackson",
                    Songs = new List<string> { "Beat It", "Billie Jean" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1982, 11, 30),
                    TotalDuration = new TimeSpan(0, 42, 19),
                    LabelId = 4
                },
                new AlbumModel
                {
                    Id = 10,
                    Name = "Led Zeppelin IV",
                    Band = "Led Zeppelin",
                    Songs = new List<string> { "Black Dog", "Stairway to Heaven" },
                    ChartPosition = 2,
                    ReleaseDate = new DateOnly(1971, 11, 8),
                    TotalDuration = new TimeSpan(0, 42, 40),
                    LabelId = 3
                },
                new AlbumModel
                {
                    Id = 11,
                    Name = "American Idiot",
                    Band = "Green Day",
                    Songs = new List<string> { "American Idiot", "Boulevard of Broken Dreams" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(2004, 9, 20),
                    TotalDuration = new TimeSpan(0, 57, 20),
                    LabelId = 1
                }
            );

            // Seed Identity roles + users
            const string ADMIN_ID      = "11111111-1111-1111-1111-111111111111";
            const string ADMIN_ROLE_ID = "22222222-2222-2222-2222-222222222222";
            const string USER_ID       = "33333333-3333-3333-3333-333333333333";
            const string USER_ROLE_ID  = "44444444-4444-4444-4444-444444444444";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "22222222-2222-2222-2222-222222222222"
                },
                new IdentityRole
                {
                    Id = USER_ROLE_ID,
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "44444444-4444-4444-4444-444444444444"
                });

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = ADMIN_ID,
                    UserName = "adam",
                    NormalizedUserName = "ADAM",
                    Email = "adam@wsei.edu.pl",
                    NormalizedEmail = "ADAM@WSEI.EDU.PL",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDGmGudXw/rANMWkCLmiue5dWTPAn+nyDe1qsPaymT8pUnfcpsEcpQbpAQSyUWhBSw==",
                    SecurityStamp = "645f4c0f-bd01-435a-b470-a29afcbf758f",
                    ConcurrencyStamp = "92de536e-bfa4-4d8c-b7f9-64fbd87d5217",
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    AccessFailedCount = 0
                },
                new IdentityUser
                {
                    Id = USER_ID,
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@wsei.edu.pl",
                    NormalizedEmail = "USER@WSEI.EDU.PL",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEMjmOhGTCE7B6YQnWW2GTOKS3b1adstju27e8RyPrLJdBZT1ubZGCPwjP/aFeuz7Iw==",
                    SecurityStamp = "cbc99d52-ca0f-4a08-bb92-ff9acfbac9b8",
                    ConcurrencyStamp = "84ceb704-dafb-4700-b710-5baf939a67f0",
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    AccessFailedCount = 0
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = ADMIN_ID, RoleId = ADMIN_ROLE_ID },
                new IdentityUserRole<string> { UserId = USER_ID,  RoleId = USER_ROLE_ID });
        }
    }
}
