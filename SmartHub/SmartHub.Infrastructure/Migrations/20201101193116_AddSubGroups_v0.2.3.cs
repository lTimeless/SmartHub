using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class AddSubGroups_v023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubGroup",
                schema: "smarthub",
                table: "Groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubGroup",
                schema: "smarthub",
                table: "Groups");
        }
    }
}
