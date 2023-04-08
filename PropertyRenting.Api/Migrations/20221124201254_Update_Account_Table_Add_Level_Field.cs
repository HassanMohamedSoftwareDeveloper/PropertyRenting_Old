using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Update_Account_Table_Add_Level_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Account");
        }
    }
}
