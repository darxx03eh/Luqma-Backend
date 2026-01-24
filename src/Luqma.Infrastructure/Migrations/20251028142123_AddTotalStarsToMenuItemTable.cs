using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalStarsToMenuItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalStars",
                table: "MenuItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_MenuItem_TotalStars_Range",
                table: "MenuItems",
                sql: "[TotalStars] >= 0 AND [TotalStars] <= 5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MenuItem_TotalStars_Range",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "TotalStars",
                table: "MenuItems");
        }
    }
}
