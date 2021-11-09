using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Web.Migrations
{
    public partial class workspacespecificcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkspaceId",
                schema: "core",
                table: "transaction_category",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_category_WorkspaceId",
                schema: "core",
                table: "transaction_category",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_category_workspace_WorkspaceId",
                schema: "core",
                table: "transaction_category",
                column: "WorkspaceId",
                principalSchema: "core",
                principalTable: "workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transaction_category_workspace_WorkspaceId",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropIndex(
                name: "IX_transaction_category_WorkspaceId",
                schema: "core",
                table: "transaction_category");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                schema: "core",
                table: "transaction_category");
        }
    }
}
