using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class basemodelchanges : Migration
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

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "core",
                table: "workspace",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "core",
                table: "user",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "transaction_category",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "core",
                table: "transaction_category",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "core",
                table: "transaction",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "core",
                table: "transaction",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "workspace");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "core",
                table: "workspace");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "user");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "core",
                table: "user");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "core",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "core",
                table: "transaction");
        }
    }
}
