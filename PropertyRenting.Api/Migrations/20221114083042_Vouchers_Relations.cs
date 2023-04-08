using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Vouchers_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerEntity",
                table: "ExchangeVoucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherDetails_AccountId",
                table: "ReceiptVoucherDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherDetails_BuildingId",
                table: "ReceiptVoucherDetails",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucherDetails_UnitId",
                table: "ReceiptVoucherDetails",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucher_AccountId",
                table: "ReceiptVoucher",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVoucher_RenterId",
                table: "ReceiptVoucher",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_AccountId",
                table: "ExchangeVoucherDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_BuildingId",
                table: "ExchangeVoucherDetails",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_UnitId",
                table: "ExchangeVoucherDetails",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucher_AccountId",
                table: "ExchangeVoucher",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucher_OwnerEntity",
                table: "ExchangeVoucher",
                column: "OwnerEntity");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Account_AccountId",
                table: "ExchangeVoucher",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Owner_OwnerEntity",
                table: "ExchangeVoucher",
                column: "OwnerEntity",
                principalTable: "Owner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucherDetails_Account_AccountId",
                table: "ExchangeVoucherDetails",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucherDetails_Building_BuildingId",
                table: "ExchangeVoucherDetails",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucherDetails_Unit_UnitId",
                table: "ExchangeVoucherDetails",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucher_Account_AccountId",
                table: "ReceiptVoucher",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucher_Renter_RenterId",
                table: "ReceiptVoucher",
                column: "RenterId",
                principalTable: "Renter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucherDetails_Account_AccountId",
                table: "ReceiptVoucherDetails",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucherDetails_Building_BuildingId",
                table: "ReceiptVoucherDetails",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptVoucherDetails_Unit_UnitId",
                table: "ReceiptVoucherDetails",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Account_AccountId",
                table: "ExchangeVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Owner_OwnerEntity",
                table: "ExchangeVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucherDetails_Account_AccountId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucherDetails_Building_BuildingId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucherDetails_Unit_UnitId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucher_Account_AccountId",
                table: "ReceiptVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucher_Renter_RenterId",
                table: "ReceiptVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucherDetails_Account_AccountId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucherDetails_Building_BuildingId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptVoucherDetails_Unit_UnitId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucherDetails_AccountId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucherDetails_BuildingId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucherDetails_UnitId",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucher_AccountId",
                table: "ReceiptVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptVoucher_RenterId",
                table: "ReceiptVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucherDetails_AccountId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucherDetails_BuildingId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucherDetails_UnitId",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucher_AccountId",
                table: "ExchangeVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucher_OwnerEntity",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "OwnerEntity",
                table: "ExchangeVoucher");
        }
    }
}
