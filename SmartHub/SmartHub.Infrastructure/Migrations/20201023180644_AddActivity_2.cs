using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class AddActivity_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateTime",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ExecutionTime",
                schema: "smarthub",
                table: "Activities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SuccessfulRequest",
                schema: "smarthub",
                table: "Activities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                schema: "smarthub",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ExecutionTime",
                schema: "smarthub",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Message",
                schema: "smarthub",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SuccessfulRequest",
                schema: "smarthub",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Username",
                schema: "smarthub",
                table: "Activities");
        }
    }
}
