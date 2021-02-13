using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class removeSubgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsSubGroup",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ParentGroupId",
                schema: "smarthub",
                table: "Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubGroup",
                schema: "smarthub",
                table: "Groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentGroupId",
                schema: "smarthub",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ParentGroupId",
                schema: "smarthub",
                table: "Groups",
                column: "ParentGroupId");

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
