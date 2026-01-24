using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequirmentItems",
                table: "RequirmentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentsOrders",
                table: "PaymentsOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuContains",
                table: "MenuContains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deductions",
                table: "Deductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryItems",
                table: "CategoryItems");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "UserAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "UserAddresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserAddresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RequirmentItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PaymentsOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderTrackings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MenuContains",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Deductions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CategoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequirmentItems",
                table: "RequirmentItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentsOrders",
                table: "PaymentsOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuContains",
                table: "MenuContains",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deductions",
                table: "Deductions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryItems",
                table: "CategoryItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_UserId",
                table: "Salaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirmentItems_ItemId",
                table: "RequirmentItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsOrders_OrderId",
                table: "PaymentsOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackings_OrderId",
                table: "OrderTrackings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuContains_ItemId",
                table: "MenuContains",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryId",
                table: "Deliveries",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_UserId",
                table: "Deductions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_CategoryId",
                table: "CategoryItems",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_UserId",
                table: "Salaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequirmentItems",
                table: "RequirmentItems");

            migrationBuilder.DropIndex(
                name: "IX_RequirmentItems_ItemId",
                table: "RequirmentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentsOrders",
                table: "PaymentsOrders");

            migrationBuilder.DropIndex(
                name: "IX_PaymentsOrders_OrderId",
                table: "PaymentsOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings");

            migrationBuilder.DropIndex(
                name: "IX_OrderTrackings_OrderId",
                table: "OrderTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuContains",
                table: "MenuContains");

            migrationBuilder.DropIndex(
                name: "IX_MenuContains_ItemId",
                table: "MenuContains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_DeliveryId",
                table: "Deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deductions",
                table: "Deductions");

            migrationBuilder.DropIndex(
                name: "IX_Deductions_UserId",
                table: "Deductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryItems",
                table: "CategoryItems");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItems_CategoryId",
                table: "CategoryItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RequirmentItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PaymentsOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderTrackings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MenuContains");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Deductions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CategoryItems");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "UserAddresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "UserAddresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserAddresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                columns: new[] { "UserId", "City", "State", "Street" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries",
                columns: new[] { "UserId", "FinanceId", "SalaryDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequirmentItems",
                table: "RequirmentItems",
                columns: new[] { "ItemId", "RequirmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentsOrders",
                table: "PaymentsOrders",
                columns: new[] { "OrderId", "PaymentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings",
                columns: new[] { "OrderId", "CustomerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                columns: new[] { "OrderId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuContains",
                table: "MenuContains",
                columns: new[] { "ItemId", "MenuId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveries",
                table: "Deliveries",
                columns: new[] { "DeliveryId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deductions",
                table: "Deductions",
                columns: new[] { "UserId", "FinanceId", "DeductionDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses",
                columns: new[] { "CustomerId", "City", "State", "Street" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryItems",
                table: "CategoryItems",
                columns: new[] { "CategoryId", "ItemId" });
        }
    }
}
