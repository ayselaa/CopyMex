using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanic.Data.Migrations
{
    public partial class addSomeChangesUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "AspNetUserRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "AspNetUserRoles");
        }
    }
}
