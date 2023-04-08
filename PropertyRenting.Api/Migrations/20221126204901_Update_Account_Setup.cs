using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Update_Account_Setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractAddionAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContributerAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RenterAccountId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_ContractAddionAccountId",
                table: "AccountSetup",
                column: "ContractAddionAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_ContributerAccountId",
                table: "AccountSetup",
                column: "ContributerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_OwnerAccountId",
                table: "AccountSetup",
                column: "OwnerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_RenterAccountId",
                table: "AccountSetup",
                column: "RenterAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_ContractAddionAccountId",
                table: "AccountSetup",
                column: "ContractAddionAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_ContributerAccountId",
                table: "AccountSetup",
                column: "ContributerAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_OwnerAccountId",
                table: "AccountSetup",
                column: "OwnerAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_RenterAccountId",
                table: "AccountSetup",
                column: "RenterAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_ContractAddionAccountId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_ContributerAccountId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_OwnerAccountId",
                table: "AccountSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_RenterAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_ContractAddionAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_ContributerAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_OwnerAccountId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_RenterAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "ContractAddionAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "ContributerAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "OwnerAccountId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "RenterAccountId",
                table: "AccountSetup");
        }
    }
}
