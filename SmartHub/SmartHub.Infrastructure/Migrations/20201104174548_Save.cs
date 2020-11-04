using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class Save : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups");

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
                name: "IsSubGroup",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "DownloadServerUrl",
                schema: "smarthub",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "smarthub",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "smarthub",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "PluginPath",
                schema: "smarthub",
                table: "Configurations");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups",
                column: "ParentGroupId",
                principalSchema: "smarthub",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups");

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

            migrationBuilder.AddColumn<bool>(
                name: "IsSubGroup",
                schema: "smarthub",
                table: "Groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DownloadServerUrl",
                schema: "smarthub",
                table: "Configurations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "smarthub",
                table: "Configurations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "smarthub",
                table: "Configurations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PluginPath",
                schema: "smarthub",
                table: "Configurations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups",
                column: "ParentGroupId",
                principalSchema: "smarthub",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
