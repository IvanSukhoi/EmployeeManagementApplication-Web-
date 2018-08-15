using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.DataEF.Migrations
{
    public partial class UpdateEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MidleName",
                table: "Employee",
                newName: "MiddleName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Employee",
                newName: "MidleName");
        }
    }
}
