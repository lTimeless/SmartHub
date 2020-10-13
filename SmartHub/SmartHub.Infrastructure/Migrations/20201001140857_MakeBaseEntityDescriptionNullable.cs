using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Persistence.Migrations
{
    public partial class MakeBaseEntityDescriptionNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Homes_HomeId",
                schema: "smarthub",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_HomeId",
                schema: "smarthub",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "HomeId",
                schema: "smarthub",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Groups",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Groups",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_HomeId",
                schema: "smarthub",
                table: "Devices",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Homes_HomeId",
                schema: "smarthub",
                table: "Devices",
                column: "HomeId",
                principalSchema: "smarthub",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
