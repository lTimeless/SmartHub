using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Persistence.Migrations
{
    public partial class RemoveAllCreatorNameProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorName",
                schema: "smarthub",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                schema: "smarthub",
                table: "Settings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Groups",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                schema: "smarthub",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
