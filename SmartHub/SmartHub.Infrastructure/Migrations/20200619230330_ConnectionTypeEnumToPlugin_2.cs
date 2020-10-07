using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class ConnectionTypeEnumToPlugin_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConnectionTypeEnum",
                table: "Plugins",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ConnectionTypeEnum",
                table: "Plugins",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
