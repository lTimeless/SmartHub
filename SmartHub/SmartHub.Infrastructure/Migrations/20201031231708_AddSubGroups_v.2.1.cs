using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class AddSubGroups_v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                schema: "smarthub",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupId",
                schema: "smarthub",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_GroupId",
                schema: "smarthub",
                table: "Groups",
                column: "GroupId",
                principalSchema: "smarthub",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_GroupId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "smarthub",
                table: "Groups");
        }
    }
}
