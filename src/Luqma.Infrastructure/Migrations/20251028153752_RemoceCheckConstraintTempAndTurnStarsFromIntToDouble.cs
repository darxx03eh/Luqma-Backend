using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luqma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoceCheckConstraintTempAndTurnStarsFromIntToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Feedback_Stars_Range",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<double>(
                name: "Stars",
                table: "Feedbacks",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Stars",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Feedback_Stars_Range",
                table: "Feedbacks",
                sql: "[Stars] >= 1 AND [Stars] <= 5");
        }
    }
}
