using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopAPI.Migrations
{
    public partial class FifthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Users_UserID",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_UserID",
                table: "orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_orders_UserID",
                table: "orders",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Users_UserID",
                table: "orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
