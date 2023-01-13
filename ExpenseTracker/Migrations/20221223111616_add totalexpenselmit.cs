using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class addtotalexpenselmit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TotalExpenseLimit",
                columns: table => new
                {
                    TotalExpenseLimitId = table.Column<int>(name: "Total_ExpenseLimit_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalExpenseLimit = table.Column<int>(name: "Total_ExpenseLimit", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalExpenseLimit", x => x.TotalExpenseLimitId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalExpenseLimit");
        }
    }
}
