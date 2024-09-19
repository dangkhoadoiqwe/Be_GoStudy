using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1723));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1721));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1718));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1716));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1714));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 16, 51, 49, 173, DateTimeKind.Utc).AddTicks(1711));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4960));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4959));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4957));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4955));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4953));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4952));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4951));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4950));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4948));
        }
    }
}
