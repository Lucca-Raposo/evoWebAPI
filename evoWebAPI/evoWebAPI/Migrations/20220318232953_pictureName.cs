using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evoWebAPI.Migrations
{
    public partial class pictureName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pictureName",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pictureName",
                table: "Employees");
        }
    }
}
