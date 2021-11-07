using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class statusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "core",
                table: "workspace",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "core",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "core",
                table: "transaction_category",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "core",
                table: "transaction",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "core",
                table: "workspace");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "core",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "core",
                table: "transaction");
        }
    }
}
