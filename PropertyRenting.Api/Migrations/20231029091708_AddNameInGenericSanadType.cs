using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class AddNameInGenericSanadType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "ReceiptVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "ExchangeVoucher",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "ReceiptVoucher");

            migrationBuilder.DropColumn(
                name: "To",
                table: "ExchangeVoucher");
        }
    }
}
