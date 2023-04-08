using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class AddUnit_MissedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitNumber",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "Unit");
        }
    }
}
