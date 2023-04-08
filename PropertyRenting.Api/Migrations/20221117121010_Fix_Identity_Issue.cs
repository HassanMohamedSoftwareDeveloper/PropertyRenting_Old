using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Fix_Identity_Issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_RenterContract_AutoNumber",
                table: "RenterContract",
                column: "AutoNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_OwnerContract_AutoNumber",
                table: "OwnerContract",
                column: "AutoNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_RenterContract_AutoNumber",
                table: "RenterContract");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_OwnerContract_AutoNumber",
                table: "OwnerContract");
        }
    }
}
