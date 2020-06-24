using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

namespace SmartHub.Infrastructure.Migrations
{
    public partial class DateTimeToInstantTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonInfo",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Instant>(
                name: "LastModifiedAt",
                table: "Settings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "CreatedAt",
                table: "Settings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "LastModifiedAt",
                table: "Plugins",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "CreatedAt",
                table: "Plugins",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "LastModifiedAt",
                table: "Homes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "CreatedAt",
                table: "Homes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "LastModifiedAt",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "CreatedAt",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "LastModifiedAt",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Instant>(
                name: "CreatedAt",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "LastModifiedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "LastModifiedAt",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Settings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Settings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Plugins",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Plugins",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Homes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Homes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Groups",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Devices",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Devices",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(Instant));

            migrationBuilder.AddColumn<string>(
                name: "PersonInfo",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
