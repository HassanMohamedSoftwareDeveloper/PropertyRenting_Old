using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_Contract_Additions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_Expense_ExpenseId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "RenterFinancialTransaction",
                newName: "ContractAdditionId");

            migrationBuilder.RenameIndex(
                name: "IX_RenterFinancialTransaction_ExpenseId",
                table: "RenterFinancialTransaction",
                newName: "IX_RenterFinancialTransaction_ContractAdditionId");

            migrationBuilder.CreateTable(
                name: "ContractAdditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAdditions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_ContractAdditions_ContractAdditionId",
                table: "RenterFinancialTransaction",
                column: "ContractAdditionId",
                principalTable: "ContractAdditions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_ContractAdditions_ContractAdditionId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropTable(
                name: "ContractAdditions");

            migrationBuilder.RenameColumn(
                name: "ContractAdditionId",
                table: "RenterFinancialTransaction",
                newName: "ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_RenterFinancialTransaction_ContractAdditionId",
                table: "RenterFinancialTransaction",
                newName: "IX_RenterFinancialTransaction_ExpenseId");

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_Expense_ExpenseId",
                table: "RenterFinancialTransaction",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");
        }
    }
}
