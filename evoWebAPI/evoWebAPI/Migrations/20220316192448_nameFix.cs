using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evoWebAPI.Migrations
{
    public partial class nameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Employees",
                newName: "departmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                newName: "IX_Employees_departmentId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Departments",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Abbrev",
                table: "Departments",
                newName: "abbrev");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Departments",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_departmentId",
                table: "Employees",
                column: "departmentId",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_departmentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "departmentId",
                table: "Employees",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_departmentId",
                table: "Employees",
                newName: "IX_Employees_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Departments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "abbrev",
                table: "Departments",
                newName: "Abbrev");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Departments",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
