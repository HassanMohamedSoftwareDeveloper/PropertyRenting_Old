using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class AddUnit_MissedFieldsP2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ACNumber",
                table: "Unit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HallNumber",
                table: "Unit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PathsNumber",
                table: "Unit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "RenterFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "RenterFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "OwnerFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "OwnerFinancialTransaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACNumber",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HallNumber",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "PathsNumber",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "OwnerFinancialTransaction");
        }
    }
}
