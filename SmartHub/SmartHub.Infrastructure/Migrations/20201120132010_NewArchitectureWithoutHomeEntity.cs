using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class NewArchitectureWithoutHomeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Homes_HomeId",
                schema: "smarthub",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Homes_HomeId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Plugins_Homes_HomeId",
                schema: "smarthub",
                table: "Plugins");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Homes_HomeId",
                schema: "smarthub",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Configurations",
                schema: "smarthub");

            migrationBuilder.DropTable(
                name: "Homes",
                schema: "smarthub");

            migrationBuilder.DropIndex(
                name: "IX_Users_HomeId",
                schema: "smarthub",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Plugins_HomeId",
                schema: "smarthub",
                table: "Plugins");

            migrationBuilder.DropIndex(
                name: "IX_Groups_HomeId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Activities_HomeId",
                schema: "smarthub",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "HomeId",
                schema: "smarthub",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HomeId",
                schema: "smarthub",
                table: "Plugins");

            migrationBuilder.DropColumn(
                name: "HomeId",
                schema: "smarthub",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "HomeId",
                schema: "smarthub",
                table: "Activities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Plugins",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeId",
                schema: "smarthub",
                table: "Activities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Homes",
                schema: "smarthub",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Address_Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Address_State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    Address_Street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValue: ""),
                    Address_ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                schema: "smarthub",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DownloadServerUrl = table.Column<string>(type: "text", nullable: false),
                    HomeId = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PluginPath = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Homes_HomeId",
                        column: x => x.HomeId,
                        principalSchema: "smarthub",
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HomeId",
                schema: "smarthub",
                table: "Users",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plugins_HomeId",
                schema: "smarthub",
                table: "Plugins",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_HomeId",
                schema: "smarthub",
                table: "Groups",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_HomeId",
                schema: "smarthub",
                table: "Activities",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_HomeId",
                schema: "smarthub",
                table: "Configurations",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_Name",
                schema: "smarthub",
                table: "Configurations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_Name",
                schema: "smarthub",
                table: "Homes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Homes_HomeId",
                schema: "smarthub",
                table: "Activities",
                column: "HomeId",
                principalSchema: "smarthub",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Homes_HomeId",
                schema: "smarthub",
                table: "Groups",
                column: "HomeId",
                principalSchema: "smarthub",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plugins_Homes_HomeId",
                schema: "smarthub",
                table: "Plugins",
                column: "HomeId",
                principalSchema: "smarthub",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Homes_HomeId",
                schema: "smarthub",
                table: "Users",
                column: "HomeId",
                principalSchema: "smarthub",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
