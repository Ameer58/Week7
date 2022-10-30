using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpartaToDo.Migrations
{
    public partial class SpartanAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpartanId",
                table: "ToDos",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_SpartanId",
                table: "ToDos",
                column: "SpartanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_AspNetUsers_SpartanId",
                table: "ToDos",
                column: "SpartanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_AspNetUsers_SpartanId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_SpartanId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "SpartanId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
