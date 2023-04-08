using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_Account_Ref_Properties_in_Voucher_Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetails_AccountId",
                table: "VoucherDetails",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetails_Account_AccountId",
                table: "VoucherDetails",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetails_Account_AccountId",
                table: "VoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetails_AccountId",
                table: "VoucherDetails");
        }
    }
}
