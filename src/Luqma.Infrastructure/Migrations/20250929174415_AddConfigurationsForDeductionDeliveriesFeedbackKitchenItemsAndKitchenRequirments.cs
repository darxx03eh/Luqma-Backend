using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationsForDeductionDeliveriesFeedbackKitchenItemsAndKitchenRequirments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "KitchenRequirments",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KitchenRequirments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "KitchenRequirments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "KitchenItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "KitchenItems",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "KitchenItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "KitchenItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Feedbacks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DeductionRate",
                table: "Deductions",
                type: "float(5)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KitchenRequirments_TotalPrice_NonNegative",
                table: "KitchenRequirments",
                sql: "[TotalPrice] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KitchenItems_Quantity_NonNegative",
                table: "KitchenItems",
                sql: "[Quantity] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Feedback_Stars_Range",
                table: "Feedbacks",
                sql: "[Stars] >= 1 AND [Stars] <= 5");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Deduction_Rate_NonNegative",
                table: "Deductions",
                sql: "[DeductionRate] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_KitchenRequirments_TotalPrice_NonNegative",
                table: "KitchenRequirments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KitchenItems_Quantity_NonNegative",
                table: "KitchenItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Feedback_Stars_Range",
                table: "Feedbacks");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Deduction_Rate_NonNegative",
                table: "Deductions");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "KitchenRequirments",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KitchenRequirments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "KitchenRequirments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "KitchenItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "KitchenItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "KitchenItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "KitchenItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DeductionRate",
                table: "Deductions",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(5)",
                oldPrecision: 5,
                oldScale: 2);
        }
    }
}
