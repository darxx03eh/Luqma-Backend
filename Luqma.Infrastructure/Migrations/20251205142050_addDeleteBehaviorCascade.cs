using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addDeleteBehaviorCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsOrders_Orders_OrderId",
                table: "PaymentsOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsOrders_Orders_OrderId",
                table: "PaymentsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsOrders_Orders_OrderId",
                table: "PaymentsOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsOrders_Orders_OrderId",
                table: "PaymentsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
