using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SomeModificationForRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequirmentItems");

            migrationBuilder.DropTable(
                name: "KitchenRequirments");

            migrationBuilder.CreateTable(
                name: "KitchenRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenRequirements", x => x.Id);
                    table.CheckConstraint("CK_KitchenRequirements_TotalPrice_NonNegative", "[TotalPrice] >= 0");
                    table.ForeignKey(
                        name: "FK_KitchenRequirements_AspNetUsers_ChefId",
                        column: x => x.ChefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequirementItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    RequirmentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float(5)", precision: 5, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequirementItems", x => x.Id);
                    table.CheckConstraint("CK_RequirementItems_Discount_Valid", "[Discount] >= 0 AND [Discount] <= 100");
                    table.CheckConstraint("CK_RequirementItems_Price_NonNegative", "[Price] >= 0");
                    table.ForeignKey(
                        name: "FK_RequirementItems_KitchenItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "KitchenItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequirementItems_KitchenRequirements_RequirmentId",
                        column: x => x.RequirmentId,
                        principalTable: "KitchenRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitchenRequirements_ChefId",
                table: "KitchenRequirements",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirementItems_ItemId",
                table: "RequirementItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirementItems_RequirmentId",
                table: "RequirementItems",
                column: "RequirmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequirementItems");

            migrationBuilder.DropTable(
                name: "KitchenRequirements");

            migrationBuilder.CreateTable(
                name: "KitchenRequirments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenRequirments", x => x.Id);
                    table.CheckConstraint("CK_KitchenRequirments_TotalPrice_NonNegative", "[TotalPrice] >= 0");
                    table.ForeignKey(
                        name: "FK_KitchenRequirments_AspNetUsers_ChefId",
                        column: x => x.ChefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequirmentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    RequirmentId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float(5)", precision: 5, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequirmentItems", x => x.Id);
                    table.CheckConstraint("CK_RequirmentItems_Discount_Valid", "[Discount] >= 0 AND [Discount] <= 100");
                    table.CheckConstraint("CK_RequirmentItems_Price_NonNegative", "[Price] >= 0");
                    table.ForeignKey(
                        name: "FK_RequirmentItems_KitchenItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "KitchenItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequirmentItems_KitchenRequirments_RequirmentId",
                        column: x => x.RequirmentId,
                        principalTable: "KitchenRequirments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitchenRequirments_ChefId",
                table: "KitchenRequirments",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirmentItems_ItemId",
                table: "RequirmentItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirmentItems_RequirmentId",
                table: "RequirmentItems",
                column: "RequirmentId");
        }
    }
}
