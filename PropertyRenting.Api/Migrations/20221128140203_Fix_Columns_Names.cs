using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Fix_Columns_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoucherNumber",
                table: "ReceiptVoucher",
                newName: "SanadNumber");

            migrationBuilder.RenameColumn(
                name: "VoucherDate",
                table: "ReceiptVoucher",
                newName: "SanadDate");

            migrationBuilder.RenameColumn(
                name: "VoucherNumber",
                table: "ExchangeVoucher",
                newName: "SanadNumber");

            migrationBuilder.RenameColumn(
                name: "VoucherDate",
                table: "ExchangeVoucher",
                newName: "SanadDate");

            migrationBuilder.AddColumn<long>(
                name: "AutoNumber",
                table: "Voucher",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "VoucherId",
                table: "Voucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Installment",
                table: "ReceiptVoucherDetails",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Installment",
                table: "ExchangeVoucherDetails",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Voucher_AutoNumber",
                table: "Voucher",
                column: "AutoNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Voucher_AutoNumber",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "AutoNumber",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Voucher");

            migrationBuilder.RenameColumn(
                name: "SanadNumber",
                table: "ReceiptVoucher",
                newName: "VoucherNumber");

            migrationBuilder.RenameColumn(
                name: "SanadDate",
                table: "ReceiptVoucher",
                newName: "VoucherDate");

            migrationBuilder.RenameColumn(
                name: "SanadNumber",
                table: "ExchangeVoucher",
                newName: "VoucherNumber");

            migrationBuilder.RenameColumn(
                name: "SanadDate",
                table: "ExchangeVoucher",
                newName: "VoucherDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "Installment",
                table: "ReceiptVoucherDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Installment",
                table: "ExchangeVoucherDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4,
                oldNullable: true);
        }
    }
}
