using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanic.Data.Migrations
{
    public partial class AddUserDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserDataFIN",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserFINs",
                columns: table => new
                {
                    FIN = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: false),
                    BloodType = table.Column<string>(type: "text", nullable: false),
                    Eyecolor = table.Column<string>(type: "text", nullable: false),
                    SosialStatus = table.Column<string>(type: "text", nullable: false),
                    Policedept = table.Column<string>(type: "text", nullable: false),
                    Series = table.Column<string>(type: "text", nullable: false),
                    Seria = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFINs", x => x.FIN);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserDataFIN",
                table: "AspNetUsers",
                column: "UserDataFIN");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserFINs_UserDataFIN",
                table: "AspNetUsers",
                column: "UserDataFIN",
                principalTable: "UserFINs",
                principalColumn: "FIN",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserFINs_UserDataFIN",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserFINs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserDataFIN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserDataFIN",
                table: "AspNetUsers");
        }
    }
}
