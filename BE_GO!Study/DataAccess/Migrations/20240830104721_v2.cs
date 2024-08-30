using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Users_CreatedBy",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_CreatedBy",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Classrooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_CreatedBy",
                table: "Classrooms",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Users_CreatedBy",
                table: "Classrooms",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
