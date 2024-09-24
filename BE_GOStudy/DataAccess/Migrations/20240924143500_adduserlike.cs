using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class adduserlike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    UserLikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.UserLikeId);
                    table.ForeignKey(
                        name: "FK_UserLikes_BlogPosts_BlogId",
                        column: x => x.BlogId,
                        principalTable: "BlogPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1528));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1526));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1525));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1523));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1519));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1516));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1514));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 14, 34, 59, 518, DateTimeKind.Utc).AddTicks(1511));

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_BlogId",
                table: "UserLikes",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_UserId",
                table: "UserLikes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikes");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6935));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6933));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6932));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6927));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6926));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6924));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6923));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6921));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 13, 20, 31, 1, DateTimeKind.Utc).AddTicks(6918));
        }
    }
}
