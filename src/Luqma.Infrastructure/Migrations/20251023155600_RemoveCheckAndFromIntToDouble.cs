using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCheckAndFromIntToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_Customers_CustomerId",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_MenuItems_ItemId",
                table: "carts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive1",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carts",
                table: "carts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "carts");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_carts_ItemId",
                table: "Carts",
                newName: "IX_Carts_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_carts_CustomerId",
                table: "Carts",
                newName: "IX_Carts_CustomerId");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Carts",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "OrderItems",
                sql: "[Quantity] > 0");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_MenuItems_ItemId",
                table: "Carts",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_MenuItems_ItemId",
                table: "Carts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "carts");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ItemId",
                table: "carts",
                newName: "IX_carts_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_CustomerId",
                table: "carts",
                newName: "IX_carts_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "carts",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carts",
                table: "carts",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive1",
                table: "OrderItems",
                sql: "[Quantity] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "carts",
                sql: "[Quantity] > 0");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_Customers_CustomerId",
                table: "carts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_MenuItems_ItemId",
                table: "carts",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
