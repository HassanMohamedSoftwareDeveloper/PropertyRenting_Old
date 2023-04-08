using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class SanadTypeId_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SandTypeId",
                table: "ReceiptVoucher",
                newName: "SanadTypeId");

            migrationBuilder.RenameColumn(
                name: "SandTypeId",
                table: "ExchangeVoucher",
                newName: "SanadTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SanadTypeId",
                table: "ReceiptVoucher",
                newName: "SandTypeId");

            migrationBuilder.RenameColumn(
                name: "SanadTypeId",
                table: "ExchangeVoucher",
                newName: "SandTypeId");
        }
    }
}
