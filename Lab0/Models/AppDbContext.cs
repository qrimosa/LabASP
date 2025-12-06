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

            // Seed Albums
            modelBuilder.Entity<AlbumModel>().HasData(
                new AlbumModel
                {
                    Id = 1,
                    Name = "Hybrid Theory",
                    Band = "Linkin Park",
                    Songs = new List<string> { "In the End", "Crawling" },
                    ChartPosition = 2,
                    ReleaseDate = new DateOnly(2000, 10, 24),
                    TotalDuration = new TimeSpan(0, 37, 45)
                },
                new AlbumModel
                {
                    Id = 2,
                    Name = "Nevermind",
                    Band = "Nirvana",
                    Songs = new List<string> { "Smells Like Teen Spirit", "Come As You Are" },
                    ChartPosition = 1,
                    ReleaseDate = new DateOnly(1991, 9, 24),
                    TotalDuration = new TimeSpan(0, 49, 23)
                });

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
