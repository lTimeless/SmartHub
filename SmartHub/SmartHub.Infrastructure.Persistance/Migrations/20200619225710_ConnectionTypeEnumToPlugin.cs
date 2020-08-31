using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class ConnectionTypeEnumToPlugin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HttpSupport",
                table: "Plugins");

            migrationBuilder.DropColumn(
                name: "MqttSupport",
                table: "Plugins");

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeEnum",
                table: "Plugins",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionTypeEnum",
                table: "Plugins");

            migrationBuilder.AddColumn<bool>(
                name: "HttpSupport",
                table: "Plugins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MqttSupport",
                table: "Plugins",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
