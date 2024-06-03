using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AStar.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddTagTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "TagsToIgnore",
            columns: table => new
            {
                Value = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => _ = table.PrimaryKey("PK_TagsToIgnore", x => x.Value));

        _ = migrationBuilder.CreateTable(
            name: "TagsToIgnoreCompletely",
            columns: table => new
            {
                Value = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => _ = table.PrimaryKey("PK_TagsToIgnoreCompletely", x => x.Value));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(name: "TagsToIgnore");

        _ = migrationBuilder.DropTable(name: "TagsToIgnoreCompletely");
    }
}
