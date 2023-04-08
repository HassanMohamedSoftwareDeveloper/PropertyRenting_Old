using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Remove_OppositeAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerFinancialTransaction_Account_OppositeAccountId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_Account_OppositeAccountId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RenterFinancialTransaction_OppositeAccountId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_OwnerFinancialTransaction_OppositeAccountId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "OppositeAccountId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "OppositeAccountId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountEntityId",
                table: "RenterFinancialTransaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountEntityId",
                table: "OwnerFinancialTransaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenterFinancialTransaction_AccountEntityId",
                table: "RenterFinancialTransaction",
                column: "AccountEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerFinancialTransaction_AccountEntityId",
                table: "OwnerFinancialTransaction",
                column: "AccountEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerFinancialTransaction_Account_AccountEntityId",
                table: "OwnerFinancialTransaction",
                column: "AccountEntityId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_Account_AccountEntityId",
                table: "RenterFinancialTransaction",
                column: "AccountEntityId",
                principalTable: "Account",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerFinancialTransaction_Account_AccountEntityId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_Account_AccountEntityId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RenterFinancialTransaction_AccountEntityId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_OwnerFinancialTransaction_AccountEntityId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "AccountEntityId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "AccountEntityId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.AddColumn<Guid>(
                name: "OppositeAccountId",
                table: "RenterFinancialTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OppositeAccountId",
                table: "OwnerFinancialTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RenterFinancialTransaction_OppositeAccountId",
                table: "RenterFinancialTransaction",
                column: "OppositeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerFinancialTransaction_OppositeAccountId",
                table: "OwnerFinancialTransaction",
                column: "OppositeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerFinancialTransaction_Account_OppositeAccountId",
                table: "OwnerFinancialTransaction",
                column: "OppositeAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_Account_OppositeAccountId",
                table: "RenterFinancialTransaction",
                column: "OppositeAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
