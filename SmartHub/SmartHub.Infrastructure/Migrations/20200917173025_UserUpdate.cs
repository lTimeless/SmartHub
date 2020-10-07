using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonInfo",
                schema: "smarthub",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonInfo",
                schema: "smarthub",
                table: "Users");
        }
    }
}
