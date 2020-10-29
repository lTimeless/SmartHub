using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class UpdateEntityForNonNullValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadServerUrl",
                schema: "smarthub",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "smarthub",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "smarthub",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PluginPath",
                schema: "smarthub",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "smarthub",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Active",
                schema: "smarthub",
                table: "Plugins");

            migrationBuilder.DropColumn(
                name: "AssemblyFilepath",
                schema: "smarthub",
                table: "Plugins");

            migrationBuilder.DropColumn(
                name: "AssemblyVersion",
                schema: "smarthub",
                table: "Plugins");

            migrationBuilder.DropColumn(
                name: "IsDownloaded",
                schema: "smarthub",
                table: "Plugins");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadServerUrl",
                schema: "smarthub",
                table: "Settings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "smarthub",
                table: "Settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "smarthub",
                table: "Settings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PluginPath",
                schema: "smarthub",
                table: "Settings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "smarthub",
                table: "Roles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "smarthub",
                table: "Plugins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AssemblyFilepath",
                schema: "smarthub",
                table: "Plugins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "AssemblyVersion",
                schema: "smarthub",
                table: "Plugins",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDownloaded",
                schema: "smarthub",
                table: "Plugins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DateTime",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExecutionTime",
                schema: "smarthub",
                table: "Activities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SuccessfulRequest",
                schema: "smarthub",
                table: "Activities",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: true);
        }
    }
}
