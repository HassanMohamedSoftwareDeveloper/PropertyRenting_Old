using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_ExpenseId_To_ReceiptVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "ReceiptVoucherDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherDetails_ExpenseId",
                table: "ReceiptVoucherDetails",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucherDetails_Expense_ExpenseId",
                table: "ReceiptVoucherDetails",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucherDetails_Expense_ExpenseId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucherDetails_ExpenseId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "ReceiptVoucherDetails");
        }
    }
}
