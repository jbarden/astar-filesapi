using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AStar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsImageAndAddIsImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TagsToIgnoreCompletely",
                type: "TEXT",
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TagsToIgnore",
                type: "TEXT",
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DirectoryName",
                table: "Files",
                type: "TEXT",
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "TEXT",
                nullable: false,
                collation: "NOCASE",
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TagsToIgnoreCompletely",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TagsToIgnore",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "DirectoryName",
                table: "Files",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldCollation: "NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldCollation: "NOCASE");
        }
    }
}
