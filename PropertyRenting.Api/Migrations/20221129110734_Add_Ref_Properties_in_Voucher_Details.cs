using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_Ref_Properties_in_Voucher_Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_BuildingId",
                table: "VoucherDetails",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_CashBankId",
                table: "VoucherDetails",
                column: "CashBankId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_ContributerId",
                table: "VoucherDetails",
                column: "ContributerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_ExpenseId",
                table: "VoucherDetails",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_OwnerId",
                table: "VoucherDetails",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_RenterId",
                table: "VoucherDetails",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_UnitId",
                table: "VoucherDetails",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Building_BuildingId",
                table: "VoucherDetails",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_CashBank_CashBankId",
                table: "VoucherDetails",
                column: "CashBankId",
                principalTable: "CashBank",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Contributer_ContributerId",
                table: "VoucherDetails",
                column: "ContributerId",
                principalTable: "Contributer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Expense_ExpenseId",
                table: "VoucherDetails",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Owner_OwnerId",
                table: "VoucherDetails",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Renter_RenterId",
                table: "VoucherDetails",
                column: "RenterId",
                principalTable: "Renter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Unit_UnitId",
                table: "VoucherDetails",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Building_BuildingId",
                table: "VoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_CashBank_CashBankId",
                table: "VoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Contributer_ContributerId",
                table: "VoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Expense_ExpenseId",
                table: "VoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Owner_OwnerId",
                table: "VoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Renter_RenterId",
                table: "VoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Unit_UnitId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_BuildingId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_CashBankId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_ContributerId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_ExpenseId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_OwnerId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_RenterId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_UnitId",
                table: "VoucherDetails");
        }
    }
}
