using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Add_Autible_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "VoucherDetails",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Voucher",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Unit",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RenterFinancialTransaction",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RenterContract",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Renter",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ReceiptVoucherDetails",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ReceiptVoucher",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OwnerFinancialTransaction",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OwnerContract",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Owner",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Nationality",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Expense",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ExchangeVoucherDetails",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ExchangeVoucher",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employee",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "District",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Country",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Contributer",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ContactPerson",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "City",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BuildingContributer",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Building",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AccountSetup",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Account",
                newName: "ModifiedBy");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VoucherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Voucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RenterFinancialTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RenterContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Renter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ReceiptVoucherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ReceiptVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OwnerFinancialTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OwnerContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Owner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Nationality",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Expense",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ExchangeVoucherDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ExchangeVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "District",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Contributer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContactPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "City",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BuildingContributer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Building",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AccountSetup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VoucherDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RenterContract");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Renter");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ReceiptVoucherDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ReceiptVoucher");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OwnerFinancialTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OwnerContract");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Nationality");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ExchangeVoucherDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ExchangeVoucher");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Contributer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContactPerson");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BuildingContributer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "VoucherDetails",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Voucher",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Unit",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RenterFinancialTransaction",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RenterContract",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Renter",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ReceiptVoucherDetails",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ReceiptVoucher",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "OwnerFinancialTransaction",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "OwnerContract",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Owner",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Nationality",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Expense",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ExchangeVoucherDetails",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ExchangeVoucher",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Employee",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "District",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Country",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Contributer",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ContactPerson",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "City",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "BuildingContributer",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Building",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "AccountSetup",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Account",
                newName: "UserId");
        }
    }
}
