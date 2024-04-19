using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanic.Data.Migrations
{
    public partial class AddContractIdForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContractId",
                table: "AspNetUsers",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contracts_ContractId",
                table: "AspNetUsers",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contracts_ContractId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContractId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "AspNetUsers");
        }
    }
}
