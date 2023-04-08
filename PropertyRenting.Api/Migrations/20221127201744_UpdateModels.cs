using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Account_AccountId",
                table: "ExchangeVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucherDetails_Account_AccountId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucher_Account_AccountId",
                table: "ReceiptVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucherDetails_Account_AccountId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucherDetails_AccountId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucherDetails_AccountId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "IsCashOrBank",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "ReceiptVoucher",
                newName: "CashBankId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptVoucher_AccountId",
                table: "ReceiptVoucher",
                newName: "IX_ReceiptVoucher_CashBankId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "ExchangeVoucher",
                newName: "CashBankId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeVoucher_AccountId",
                table: "ExchangeVoucher",
                newName: "IX_ExchangeVoucher_CashBankId");

            migrationBuilder.AddColumn<Guid>(
                name: "CashBankId",
                table: "VoucherDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContributerId",
                table: "VoucherDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "VoucherDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContributerId",
                table: "ReceiptVoucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ReceiptVoucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SandTypeId",
                table: "ReceiptVoucher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "ExchangeVoucherDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContributerId",
                table: "ExchangeVoucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RenterId",
                table: "ExchangeVoucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SandTypeId",
                table: "ExchangeVoucher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CashBank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashBank", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucher_ContributerId",
                table: "ReceiptVoucher",
                column: "ContributerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucher_OwnerId",
                table: "ReceiptVoucher",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_ExpenseId",
                table: "ExchangeVoucherDetails",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucher_ContributerId",
                table: "ExchangeVoucher",
                column: "ContributerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucher_RenterId",
                table: "ExchangeVoucher",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_CashBank_CashBankId",
                table: "ExchangeVoucher",
                column: "CashBankId",
                principalTable: "CashBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Contributer_ContributerId",
                table: "ExchangeVoucher",
                column: "ContributerId",
                principalTable: "Contributer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Renter_RenterId",
                table: "ExchangeVoucher",
                column: "RenterId",
                principalTable: "Renter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucherDetails_Expense_ExpenseId",
                table: "ExchangeVoucherDetails",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucher_CashBank_CashBankId",
                table: "ReceiptVoucher",
                column: "CashBankId",
                principalTable: "CashBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucher_Contributer_ContributerId",
                table: "ReceiptVoucher",
                column: "ContributerId",
                principalTable: "Contributer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucher_Owner_OwnerId",
                table: "ReceiptVoucher",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_CashBank_CashBankId",
                table: "ExchangeVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Contributer_ContributerId",
                table: "ExchangeVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Renter_RenterId",
                table: "ExchangeVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucherDetails_Expense_ExpenseId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucher_CashBank_CashBankId",
                table: "ReceiptVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucher_Contributer_ContributerId",
                table: "ReceiptVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucher_Owner_OwnerId",
                table: "ReceiptVoucher");

            migrationBuilder.DropTable(
                name: "CashBank");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucher_ContributerId",
                table: "ReceiptVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucher_OwnerId",
                table: "ReceiptVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucherDetails_ExpenseId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucher_ContributerId",
                table: "ExchangeVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucher_RenterId",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "CashBankId",
                table: "VoucherDetails");

            migrationBuilder.DropColumn(
                name: "ContributerId",
                table: "VoucherDetails");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "VoucherDetails");

            migrationBuilder.DropColumn(
                name: "ContributerId",
                table: "ReceiptVoucher");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ReceiptVoucher");

            migrationBuilder.DropColumn(
                name: "SandTypeId",
                table: "ReceiptVoucher");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "ContributerId",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "SandTypeId",
                table: "ExchangeVoucher");

            migrationBuilder.RenameColumn(
                name: "CashBankId",
                table: "ReceiptVoucher",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptVoucher_CashBankId",
                table: "ReceiptVoucher",
                newName: "IX_ReceiptVoucher_AccountId");

            migrationBuilder.RenameColumn(
                name: "CashBankId",
                table: "ExchangeVoucher",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeVoucher_CashBankId",
                table: "ExchangeVoucher",
                newName: "IX_ExchangeVoucher_AccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "ReceiptVoucherDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "ExchangeVoucherDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCashOrBank",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherDetails_AccountId",
                table: "ReceiptVoucherDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_AccountId",
                table: "ExchangeVoucherDetails",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Account_AccountId",
                table: "ExchangeVoucher",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucherDetails_Account_AccountId",
                table: "ExchangeVoucherDetails",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucher_Account_AccountId",
                table: "ReceiptVoucher",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucherDetails_Account_AccountId",
                table: "ReceiptVoucherDetails",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
