using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class AddSubGroups_v22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_GroupId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "smarthub",
                table: "Groups",
                newName: "ParentGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_GroupId",
                schema: "smarthub",
                table: "Groups",
                newName: "IX_Groups_ParentGroupId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "ParentGroupId",
                schema: "smarthub",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups",
                newName: "IX_Groups_GroupId");

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
    }
}
