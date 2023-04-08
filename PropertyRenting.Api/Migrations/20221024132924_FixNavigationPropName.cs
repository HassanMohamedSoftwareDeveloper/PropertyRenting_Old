using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class FixNavigationPropName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RenterFinancialTransaction_CreatedOnUtc",
                table: "RenterFinancialTransaction");

            migrationBuilder.DropIndex(
                name: "IX_OwnerFinancialTransaction_CreatedOnUtc",
                table: "OwnerFinancialTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RenterFinancialTransaction_CreatedOnUtc",
                table: "RenterFinancialTransaction",
                column: "CreatedOnUtc");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerFinancialTransaction_CreatedOnUtc",
                table: "OwnerFinancialTransaction",
                column: "CreatedOnUtc");
        }
    }
}
