using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Remove_OppositeAccount1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
