using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class basemodelchangs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Constant",
                schema: "core",
                table: "workspace",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Constant",
                schema: "core",
                table: "user",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Constant",
                schema: "core",
                table: "transaction_category",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Constant",
                schema: "core",
                table: "transaction",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "core",
                table: "workspace",
                newName: "Constant");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "core",
                table: "user",
                newName: "Constant");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "core",
                table: "transaction_category",
                newName: "Constant");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "core",
                table: "transaction",
                newName: "Constant");
        }
    }
}
