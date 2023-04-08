using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Exchange_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "InstallmentId",
                table: "ExchangeVoucher");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "ExchangeVoucher",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExchangeVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VoucherDate",
                table: "ExchangeVoucher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VoucherNumber",
                table: "ExchangeVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExchangeVoucherDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExchangeVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeVoucherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeVoucherDetails_ExchangeVoucher_ExchangeVoucherId",
                        column: x => x.ExchangeVoucherId,
                        principalTable: "ExchangeVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeVoucherDetails_ExchangeVoucherId",
                table: "ExchangeVoucherDetails",
                column: "ExchangeVoucherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "VoucherDate",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "VoucherNumber",
                table: "ExchangeVoucher");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseTypeId",
                table: "ExchangeVoucher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentId",
                table: "ExchangeVoucher",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
