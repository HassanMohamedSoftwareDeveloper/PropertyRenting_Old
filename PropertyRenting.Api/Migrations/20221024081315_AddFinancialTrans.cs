using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class AddFinancialTrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RenterFinancialTransaction_ContractId",
                table: "RenterFinancialTransaction",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerFinancialTransaction_ContractId",
                table: "OwnerFinancialTransaction",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerFinancialTransaction_OwnerContract_ContractId",
                table: "OwnerFinancialTransaction",
                column: "ContractId",
                principalTable: "OwnerContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_RenterContract_ContractId",
                table: "RenterFinancialTransaction",
                column: "ContractId",
                principalTable: "RenterContract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerFinancialTransaction_OwnerContract_ContractId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_RenterContract_ContractId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RenterFinancialTransaction_ContractId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_OwnerFinancialTransaction_ContractId",
                table: "OwnerFinancialTransaction");
        }
    }
}
