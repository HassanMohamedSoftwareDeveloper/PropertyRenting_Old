using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_fields_Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_ContractAddionAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_ContractAddionAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "ContractAddionAccountId",
                table: "AccountSetup");

            migrationBuilder.RenameColumn(
                name: "SanadManualNumber",
                table: "Voucher",
                newName: "ReferenceManualNumber");

            migrationBuilder.RenameColumn(
                name: "SanadAutoNumber",
                table: "Voucher",
                newName: "ReferenceAutoNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Expense",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AdditionId",
                table: "ExchangeVoucherDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "ContractAdditions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountEntityId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_AdditionId",
                table: "ExchangeVoucherDetails",
                column: "AdditionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_AccountEntityId",
                table: "AccountSetup",
                column: "AccountEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_AccountEntityId",
                table: "AccountSetup",
                column: "AccountEntityId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucherDetails_ContractAdditions_AdditionId",
                table: "ExchangeVoucherDetails",
                column: "AdditionId",
                principalTable: "ContractAdditions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_AccountEntityId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucherDetails_ContractAdditions_AdditionId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucherDetails_AdditionId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_AccountEntityId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "AdditionId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ContractAdditions");

            migrationBuilder.DropColumn(
                name: "AccountEntityId",
                table: "AccountSetup");

            migrationBuilder.RenameColumn(
                name: "ReferenceManualNumber",
                table: "Voucher",
                newName: "SanadManualNumber");

            migrationBuilder.RenameColumn(
                name: "ReferenceAutoNumber",
                table: "Voucher",
                newName: "SanadAutoNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractAddionAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_ContractAddionAccountId",
                table: "AccountSetup",
                column: "ContractAddionAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_ContractAddionAccountId",
                table: "AccountSetup",
                column: "ContractAddionAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
