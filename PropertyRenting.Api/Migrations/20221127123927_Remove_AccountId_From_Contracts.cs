using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Remove_AccountId_From_Contracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerFinancialTransaction_Account_AccountId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_RenterFinancialTransaction_Account_AccountId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RenterFinancialTransaction_AccountId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_OwnerFinancialTransaction_AccountId",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "OwnerFinancialTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "RenterFinancialTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "OwnerFinancialTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RenterFinancialTransaction_AccountId",
                table: "RenterFinancialTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerFinancialTransaction_AccountId",
                table: "OwnerFinancialTransaction",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerFinancialTransaction_Account_AccountId",
                table: "OwnerFinancialTransaction",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RenterFinancialTransaction_Account_AccountId",
                table: "RenterFinancialTransaction",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
