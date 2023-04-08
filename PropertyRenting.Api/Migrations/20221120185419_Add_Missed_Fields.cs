using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_Missed_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SanadAutoNumber",
                table: "Voucher",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanadManualNumber",
                table: "Voucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AutoNumber",
                table: "ReceiptVoucher",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "AutoNumber",
                table: "ExchangeVoucher",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ReceiptVoucher_AutoNumber",
                table: "ReceiptVoucher",
                column: "AutoNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ExchangeVoucher_AutoNumber",
                table: "ExchangeVoucher",
                column: "AutoNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ReceiptVoucher_AutoNumber",
                table: "ReceiptVoucher");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ExchangeVoucher_AutoNumber",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "SanadAutoNumber",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "SanadManualNumber",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "AutoNumber",
                table: "ReceiptVoucher");

            migrationBuilder.DropColumn(
                name: "AutoNumber",
                table: "ExchangeVoucher");
        }
    }
}
