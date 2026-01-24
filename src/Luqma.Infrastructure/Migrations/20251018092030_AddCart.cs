using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CashierId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CashierId",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CashierId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "LuqmaUserId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Id);
                    table.CheckConstraint("CK_OrderItem_Quantity_Positive", "[Quantity] > 0");
                    table.ForeignKey(
                        name: "FK_carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carts_MenuItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LuqmaUserId",
                table: "Orders",
                column: "LuqmaUserId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive1",
                table: "OrderItems",
                sql: "[Quantity] > 0");

            migrationBuilder.CreateIndex(
                name: "IX_carts_CustomerId",
                table: "carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_carts_ItemId",
                table: "carts",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_LuqmaUserId",
                table: "Orders",
                column: "LuqmaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_LuqmaUserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LuqmaUserId",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "LuqmaUserId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CashierId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                column: "CashierId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "OrderItems",
                sql: "[Quantity] > 0");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CashierId",
                table: "Orders",
                column: "CashierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
