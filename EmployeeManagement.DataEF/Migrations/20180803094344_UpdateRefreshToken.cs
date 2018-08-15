using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.DataEF.Migrations
{
    public partial class UpdateRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JwtRefreshToken",
                table: "Token",
                newName: "JsonWebRefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonWebRefreshToken",
                table: "Token",
                newName: "JwtRefreshToken");
        }
    }
}
