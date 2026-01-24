using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestoreConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_LuqmaUserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "LuqmaUserId",
                table: "Orders",
                newName: "CashierId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_LuqmaUserId",
                table: "Orders",
                newName: "IX_Orders_CashierId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CashierId",
                table: "Orders",
                column: "CashierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CashierId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CashierId",
                table: "Orders",
                newName: "LuqmaUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                newName: "IX_Orders_LuqmaUserId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_LuqmaUserId",
                table: "Orders",
                column: "LuqmaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
