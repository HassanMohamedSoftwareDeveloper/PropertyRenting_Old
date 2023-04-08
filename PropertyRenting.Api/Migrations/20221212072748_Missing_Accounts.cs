using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Missing_Accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RevenueAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_ExpenseAccountId",
                table: "AccountSetup",
                column: "ExpenseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_RevenueAccountId",
                table: "AccountSetup",
                column: "RevenueAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_ExpenseAccountId",
                table: "AccountSetup",
                column: "ExpenseAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_RevenueAccountId",
                table: "AccountSetup",
                column: "RevenueAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_ExpenseAccountId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_RevenueAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_ExpenseAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_RevenueAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "ExpenseAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "RevenueAccountId",
                table: "AccountSetup");
        }
    }
}
