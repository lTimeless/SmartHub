using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class DeletedSettingsUnusedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WatchPathAbsolut",
                schema: "smarthub",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WatchPathAbsolut",
                schema: "smarthub",
                table: "Settings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
