using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evoWebAPI.Migrations
{
    public partial class imageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imageModels");

            migrationBuilder.AddColumn<string>(
                name: "picture",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "picture",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "imageModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    imageName = table.Column<string>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imageModels", x => x.id);
                });
        }
    }
}
