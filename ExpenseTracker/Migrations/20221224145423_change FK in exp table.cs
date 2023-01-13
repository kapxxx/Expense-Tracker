using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class changeFKinexptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryC_Id",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CategoryC_Id",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CategoryC_Id",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "E_Title",
                table: "Expenses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "C_Name",
                table: "Categories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_C_id",
                table: "Expenses",
                column: "C_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_C_id",
                table: "Expenses",
                column: "C_id",
                principalTable: "Categories",
                principalColumn: "C_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_C_id",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_C_id",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "E_Title",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "CategoryC_Id",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "C_Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryC_Id",
                table: "Expenses",
                column: "CategoryC_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryC_Id",
                table: "Expenses",
                column: "CategoryC_Id",
                principalTable: "Categories",
                principalColumn: "C_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
