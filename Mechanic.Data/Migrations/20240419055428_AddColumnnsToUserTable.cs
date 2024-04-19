using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanic.Data.Migrations
{
    public partial class AddColumnnsToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryDriverLicence",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDateDriverLincence",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDateDriverLicence",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryDriverLicence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpirationDateDriverLincence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IssueDateDriverLicence",
                table: "AspNetUsers");
        }
    }
}
