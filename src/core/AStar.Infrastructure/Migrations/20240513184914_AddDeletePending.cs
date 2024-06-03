using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AStar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletePending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<bool>(
                name: "DeletePending",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
                name: "DeletePending",
                table: "Files");
    }
}
