using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class modelinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "workspace",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "workspace",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "transaction_category",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "transaction_category",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "transaction",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "transaction",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "workspace");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "workspace");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "user");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "user");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "transaction");
        }
    }
}
