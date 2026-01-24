using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateFeedbacksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTracking_Customers_CustomerId",
                table: "OrderTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTracking_Orders_OrderId",
                table: "OrderTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsOrder_Orders_OrderId",
                table: "PaymentsOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsOrder_Payments_PaymentId",
                table: "PaymentsOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentsOrder",
                table: "PaymentsOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTracking",
                table: "OrderTracking");

            migrationBuilder.RenameTable(
                name: "PaymentsOrder",
                newName: "PaymentsOrders");

            migrationBuilder.RenameTable(
                name: "OrderTracking",
                newName: "OrderTrackings");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentsOrder_PaymentId",
                table: "PaymentsOrders",
                newName: "IX_PaymentsOrders_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTracking_CustomerId",
                table: "OrderTrackings",
                newName: "IX_OrderTrackings_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentsOrders",
                table: "PaymentsOrders",
                columns: new[] { "OrderId", "PaymentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings",
                columns: new[] { "OrderId", "CustomerId" });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_MenuItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedbacks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ItemId",
                table: "Feedbacks",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackings_Customers_CustomerId",
                table: "OrderTrackings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackings_Orders_OrderId",
                table: "OrderTrackings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsOrders_Orders_OrderId",
                table: "PaymentsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsOrders_Payments_PaymentId",
                table: "PaymentsOrders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackings_Customers_CustomerId",
                table: "OrderTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackings_Orders_OrderId",
                table: "OrderTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsOrders_Orders_OrderId",
                table: "PaymentsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentsOrders_Payments_PaymentId",
                table: "PaymentsOrders");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentsOrders",
                table: "PaymentsOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings");

            migrationBuilder.RenameTable(
                name: "PaymentsOrders",
                newName: "PaymentsOrder");

            migrationBuilder.RenameTable(
                name: "OrderTrackings",
                newName: "OrderTracking");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentsOrders_PaymentId",
                table: "PaymentsOrder",
                newName: "IX_PaymentsOrder_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTrackings_CustomerId",
                table: "OrderTracking",
                newName: "IX_OrderTracking_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentsOrder",
                table: "PaymentsOrder",
                columns: new[] { "OrderId", "PaymentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTracking",
                table: "OrderTracking",
                columns: new[] { "OrderId", "CustomerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTracking_Customers_CustomerId",
                table: "OrderTracking",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTracking_Orders_OrderId",
                table: "OrderTracking",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsOrder_Orders_OrderId",
                table: "PaymentsOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentsOrder_Payments_PaymentId",
                table: "PaymentsOrder",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
