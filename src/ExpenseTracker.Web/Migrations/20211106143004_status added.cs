using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class statusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "workspace",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusConstants",
                schema: "core",
                table: "transaction_category",
                type: "text",
                nullable: false,
                defaultValue: "");

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
                name: "StatusConstants",
                schema: "core",
                table: "workspace");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "user");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "StatusConstants",
                schema: "core",
                table: "transaction");
        }
    }
}
