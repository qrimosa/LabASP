using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab0.Migrations
{
    /// <inheritdoc />
    public partial class AddLabelAndAlbumRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Albums",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                column: "LabelId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2,
                column: "LabelId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Labels",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "USA", "Warner Bros. Records" },
                    { 2, "USA", "DGC Records" },
                    { 3, "UK", "EMI" },
                    { 4, "Japan", "Sony Music" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Band", "ChartPosition", "LabelId", "Name", "ReleaseDate", "Songs", "TotalDuration" },
                values: new object[,]
                {
                    { 3, "Linkin Park", 1, 1, "Meteora", "2003-03-25", "Numb;Somewhere I Belong", "00:36:35" },
                    { 4, "AC/DC", 1, 3, "Back in Black", "1980-07-25", "Hells Bells;Back in Black", "00:42:11" },
                    { 5, "Radiohead", 1, 3, "OK Computer", "1997-05-21", "Paranoid Android;Karma Police", "00:53:27" },
                    { 6, "Red Hot Chili Peppers", 3, 4, "Californication", "1999-06-08", "Scar Tissue;Otherside", "00:56:24" },
                    { 7, "Pink Floyd", 1, 3, "The Dark Side of the Moon", "1973-03-01", "Time;Money", "00:42:49" },
                    { 8, "The Beatles", 1, 3, "Abbey Road", "1969-09-26", "Come Together;Something", "00:47:23" },
                    { 9, "Michael Jackson", 1, 4, "Thriller", "1982-11-30", "Beat It;Billie Jean", "00:42:19" },
                    { 10, "Led Zeppelin", 2, 3, "Led Zeppelin IV", "1971-11-08", "Black Dog;Stairway to Heaven", "00:42:40" },
                    { 11, "Green Day", 1, 1, "American Idiot", "2004-09-20", "American Idiot;Boulevard of Broken Dreams", "00:57:20" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_LabelId",
                table: "Albums",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Labels_LabelId",
                table: "Albums",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Labels_LabelId",
                table: "Albums");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Albums_LabelId",
                table: "Albums");

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Albums");
        }
    }
}
