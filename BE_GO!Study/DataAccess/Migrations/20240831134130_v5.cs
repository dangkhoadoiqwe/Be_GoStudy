using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialization_Specializations_SpecializationId",
                table: "UserSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialization_Users_UserId",
                table: "UserSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecialization",
                table: "UserSpecialization");

            migrationBuilder.RenameTable(
                name: "UserSpecialization",
                newName: "UserSpecializations");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialization_UserId",
                table: "UserSpecializations",
                newName: "IX_UserSpecializations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialization_SpecializationId",
                table: "UserSpecializations",
                newName: "IX_UserSpecializations_SpecializationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations",
                column: "UserSpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecializations_Specializations_SpecializationId",
                table: "UserSpecializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecializations_Users_UserId",
                table: "UserSpecializations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecializations_Specializations_SpecializationId",
                table: "UserSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecializations_Users_UserId",
                table: "UserSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations");

            migrationBuilder.RenameTable(
                name: "UserSpecializations",
                newName: "UserSpecialization");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecializations_UserId",
                table: "UserSpecialization",
                newName: "IX_UserSpecialization_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecializations_SpecializationId",
                table: "UserSpecialization",
                newName: "IX_UserSpecialization_SpecializationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecialization",
                table: "UserSpecialization",
                column: "UserSpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialization_Specializations_SpecializationId",
                table: "UserSpecialization",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialization_Users_UserId",
                table: "UserSpecialization",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
