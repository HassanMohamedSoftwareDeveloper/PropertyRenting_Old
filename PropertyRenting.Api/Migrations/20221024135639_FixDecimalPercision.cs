using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class FixDecimalPercision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalArea",
                table: "Unit",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "RentableArea",
                table: "Unit",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualRentAmount",
                table: "Unit",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RenterFinancialTransaction",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "IncreasingValue",
                table: "RenterContract",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "RenterContract",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "OwnerFinancialTransaction",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "OwnerContract",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                table: "BuildingContributer",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "YearRentAmount",
                table: "Building",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "YearReRentAmount",
                table: "Building",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalArea",
                table: "Building",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "RentableArea",
                table: "Building",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldPrecision: 20,
                oldScale: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalArea",
                table: "Unit",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "RentableArea",
                table: "Unit",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualRentAmount",
                table: "Unit",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RenterFinancialTransaction",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "IncreasingValue",
                table: "RenterContract",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "RenterContract",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "OwnerFinancialTransaction",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "OwnerContract",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                table: "BuildingContributer",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "YearRentAmount",
                table: "Building",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "YearReRentAmount",
                table: "Building",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalArea",
                table: "Building",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "RentableArea",
                table: "Building",
                type: "decimal(20,2)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldPrecision: 20,
                oldScale: 4);
        }
    }
}
