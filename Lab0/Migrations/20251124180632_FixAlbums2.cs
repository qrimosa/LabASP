using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab0.Migrations
{
    /// <inheritdoc />
    public partial class FixAlbums2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReleaseDate", "TotalDuration" },
                values: new object[] { "2000-10-24", "00:37:45" });

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReleaseDate", "TotalDuration" },
                values: new object[] { "1991-09-24", "00:49:23" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ReleaseDate", "TotalDuration" },
                values: new object[] { new DateOnly(2000, 10, 24), new TimeSpan(0, 0, 37, 45, 0) });

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ReleaseDate", "TotalDuration" },
                values: new object[] { new DateOnly(1991, 9, 24), new TimeSpan(0, 0, 49, 23, 0) });
        }
    }
}
