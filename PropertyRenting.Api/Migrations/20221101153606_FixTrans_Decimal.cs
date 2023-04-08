using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class FixTrans_Decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "RenterFinancialTransaction",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "RenterFinancialTransaction",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "OwnerFinancialTransaction",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "OwnerFinancialTransaction",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "RenterFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "RenterFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "OwnerFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "OwnerFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);
        }
    }
}
