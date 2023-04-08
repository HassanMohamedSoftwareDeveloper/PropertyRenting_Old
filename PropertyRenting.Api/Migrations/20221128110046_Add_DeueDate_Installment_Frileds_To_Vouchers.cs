using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_DeueDate_Installment_Frileds_To_Vouchers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "ReceiptVoucherDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Installment",
                table: "ReceiptVoucherDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "ExchangeVoucherDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Installment",
                table: "ExchangeVoucherDetails",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropColumn(
                name: "Installment",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "Installment",
                table: "ExchangeVoucherDetails");
        }
    }
}
