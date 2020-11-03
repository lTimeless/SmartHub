using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class AddM2MforGroupsAndDevices_v024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Groups_GroupId",
                schema: "smarthub",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_GroupId",
                schema: "smarthub",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "smarthub",
                table: "Devices");

            migrationBuilder.CreateTable(
                name: "GroupsDevices",
                schema: "smarthub",
                columns: table => new
                {
                    DevicesId = table.Column<string>(type: "text", nullable: false),
                    GroupsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsDevices", x => new { x.DevicesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_GroupsDevices_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalSchema: "smarthub",
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsDevices_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalSchema: "smarthub",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsDevices_GroupsId",
                schema: "smarthub",
                table: "GroupsDevices",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsDevices",
                schema: "smarthub");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                schema: "smarthub",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GroupId",
                schema: "smarthub",
                table: "Devices",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Groups_GroupId",
                schema: "smarthub",
                table: "Devices",
                column: "GroupId",
                principalSchema: "smarthub",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
