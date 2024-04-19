using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanic.Data.Migrations
{
    public partial class addNullForUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserFINs_UserDataFIN",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserDataFIN",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserFINs_UserDataFIN",
                table: "AspNetUsers",
                column: "UserDataFIN",
                principalTable: "UserFINs",
                principalColumn: "FIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserFINs_UserDataFIN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tests");

            migrationBuilder.AlterColumn<string>(
                name: "UserDataFIN",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserFINs_UserDataFIN",
                table: "AspNetUsers",
                column: "UserDataFIN",
                principalTable: "UserFINs",
                principalColumn: "FIN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
