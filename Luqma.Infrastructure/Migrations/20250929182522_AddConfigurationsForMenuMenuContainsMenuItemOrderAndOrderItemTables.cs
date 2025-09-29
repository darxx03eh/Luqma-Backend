using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationsForMenuMenuContainsMenuItemOrderAndOrderItemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_MenuItem_ItemId",
                table: "CategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_MenuItem_ItemId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuContains_MenuItem_ItemId",
                table: "MenuContains");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_MenuItem_ItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_MenuItem_ItemId",
                table: "Predictions");

            migrationBuilder.DropForeignKey(
                name: "FK_WasteReports_MenuItem_ItemId",
                table: "WasteReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem");

            migrationBuilder.RenameTable(
                name: "MenuItem",
                newName: "MenuItems");

            migrationBuilder.AlterColumn<string>(
                name: "WeatherConditions",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventTag",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DayOfWeek",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerType",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "OrderItems",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Menus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menus",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "MenuItems",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "MenuItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "MenuItems",
                type: "float(5)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_TotalPrice_NonNegative",
                table: "Orders",
                sql: "[TotalPrice] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "OrderItems",
                sql: "[Quantity] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MenuItem_Discount_Valid",
                table: "MenuItems",
                sql: "[Discount] IS NULL OR [Discount] >= 0 AND [Discount] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MenuItem_Price_NonNegative",
                table: "MenuItems",
                sql: "[Price] >= 0");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_MenuItems_ItemId",
                table: "CategoryItems",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_MenuItems_ItemId",
                table: "Feedbacks",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuContains_MenuItems_ItemId",
                table: "MenuContains",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_MenuItems_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_MenuItems_ItemId",
                table: "Predictions",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WasteReports_MenuItems_ItemId",
                table: "WasteReports",
                column: "ItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_MenuItems_ItemId",
                table: "CategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_MenuItems_ItemId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuContains_MenuItems_ItemId",
                table: "MenuContains");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_MenuItems_ItemId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_MenuItems_ItemId",
                table: "Predictions");

            migrationBuilder.DropForeignKey(
                name: "FK_WasteReports_MenuItems_ItemId",
                table: "WasteReports");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_TotalPrice_NonNegative",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity_Positive",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_MenuItem_Discount_Valid",
                table: "MenuItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_MenuItem_Price_NonNegative",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItem");

            migrationBuilder.AlterColumn<string>(
                name: "WeatherConditions",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventTag",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DayOfWeek",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerType",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "OrderItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "MenuItem",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "MenuItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "MenuItem",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(5)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MenuItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_MenuItem_ItemId",
                table: "CategoryItems",
                column: "ItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_MenuItem_ItemId",
                table: "Feedbacks",
                column: "ItemId",
                principalTable: "MenuItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuContains_MenuItem_ItemId",
                table: "MenuContains",
                column: "ItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_MenuItem_ItemId",
                table: "OrderItems",
                column: "ItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_MenuItem_ItemId",
                table: "Predictions",
                column: "ItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WasteReports_MenuItem_ItemId",
                table: "WasteReports",
                column: "ItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
