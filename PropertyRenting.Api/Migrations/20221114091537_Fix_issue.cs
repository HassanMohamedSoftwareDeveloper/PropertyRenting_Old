using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Fix_issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Owner_OwnerEntity",
                table: "ExchangeVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucher_OwnerEntity",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "OwnerEntity",
                table: "ExchangeVoucher");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucher_OwnerId",
                table: "ExchangeVoucher",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Owner_OwnerId",
                table: "ExchangeVoucher",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeVoucher_Owner_OwnerId",
                table: "ExchangeVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeVoucher_OwnerId",
                table: "ExchangeVoucher");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerEntity",
                table: "ExchangeVoucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucher_OwnerEntity",
                table: "ExchangeVoucher",
                column: "OwnerEntity");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeVoucher_Owner_OwnerEntity",
                table: "ExchangeVoucher",
                column: "OwnerEntity",
                principalTable: "Owner",
                principalColumn: "Id");
        }
    }
}
