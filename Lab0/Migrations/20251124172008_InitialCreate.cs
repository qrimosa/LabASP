using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab0.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Band = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Songs = table.Column<string>(type: "TEXT", nullable: true),
                    ChartPosition = table.Column<int>(type: "INTEGER", nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TotalDuration = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Band", "ChartPosition", "Name", "ReleaseDate", "Songs", "TotalDuration" },
                values: new object[,]
                {
                    { 1, "Linkin Park", 2, "Hybrid Theory", new DateOnly(2000, 10, 24), "In the End;Crawling", new TimeSpan(0, 0, 37, 45, 0) },
                    { 2, "Nirvana", 1, "Nevermind", new DateOnly(1991, 9, 24), "Smells Like Teen Spirit;Come As You Are", new TimeSpan(0, 0, 49, 23, 0) }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "adam@wsei.edu.pl", "Adam" },
                    { 2, "ewa@wsei.edu.pl", "Ewa" },
                    { 3, "karol@op.pl", "Karol" }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 101, "Św. Filipa 17, Kraków", "WSEI" },
                    { 102, "Polska", "PKP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
