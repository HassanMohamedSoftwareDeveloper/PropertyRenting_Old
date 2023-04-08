using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_Trans_Expense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "OwnerFinancialTransaction");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "RenterFinancialTransaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenterFinancialTransaction_ExpenseId",
                table: "RenterFinancialTransaction",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_Expense_ExpenseId",
                table: "RenterFinancialTransaction",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_Expense_ExpenseId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RenterFinancialTransaction_ExpenseId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "RenterFinancialTransaction");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RenterFinancialTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "OwnerFinancialTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
