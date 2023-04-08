using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    public partial class change_fields_name1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountSetup_Account_AccountEntityId",
                table: "AccountSetup");

            migrationBuilder.DropIndex(
                name: "IX_AccountSetup_AccountEntityId",
                table: "AccountSetup");

            migrationBuilder.DropColumn(
                name: "AccountEntityId",
                table: "AccountSetup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountEntityId",
                table: "AccountSetup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSetup_AccountEntityId",
                table: "AccountSetup",
                column: "AccountEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSetup_Account_AccountEntityId",
                table: "AccountSetup",
                column: "AccountEntityId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
