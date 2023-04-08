using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class change_fields_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_ExpenseAccountId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_RevenueAccountId",
                table: "AccountSetup");

            migrationBuilder.RenameColumn(
                name: "RevenueAccountId",
                table: "AccountSetup",
                newName: "AccruedRevenueAccountId");

            migrationBuilder.RenameColumn(
                name: "ExpenseAccountId",
                table: "AccountSetup",
                newName: "AccruedExpenseAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSetup_RevenueAccountId",
                table: "AccountSetup",
                newName: "IX_AccountSetup_AccruedRevenueAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSetup_ExpenseAccountId",
                table: "AccountSetup",
                newName: "IX_AccountSetup_AccruedExpenseAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_AccruedExpenseAccountId",
                table: "AccountSetup",
                column: "AccruedExpenseAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_AccruedRevenueAccountId",
                table: "AccountSetup",
                column: "AccruedRevenueAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_AccruedExpenseAccountId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_AccruedRevenueAccountId",
                table: "AccountSetup");

            migrationBuilder.RenameColumn(
                name: "AccruedRevenueAccountId",
                table: "AccountSetup",
                newName: "RevenueAccountId");

            migrationBuilder.RenameColumn(
                name: "AccruedExpenseAccountId",
                table: "AccountSetup",
                newName: "ExpenseAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSetup_AccruedRevenueAccountId",
                table: "AccountSetup",
                newName: "IX_AccountSetup_RevenueAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountSetup_AccruedExpenseAccountId",
                table: "AccountSetup",
                newName: "IX_AccountSetup_ExpenseAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_ExpenseAccountId",
                table: "AccountSetup",
                column: "ExpenseAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_RevenueAccountId",
                table: "AccountSetup",
                column: "RevenueAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
