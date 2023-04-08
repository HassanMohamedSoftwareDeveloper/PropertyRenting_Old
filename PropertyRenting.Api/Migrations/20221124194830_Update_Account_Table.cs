using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class Update_Account_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Account",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Account_Code",
                table: "Account",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentId",
                table: "Account",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Account_ParentId",
                table: "Account",
                column: "ParentId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Account_ParentId",
                table: "Account");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Account_Code",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ParentId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Account");
        }
    }
}
