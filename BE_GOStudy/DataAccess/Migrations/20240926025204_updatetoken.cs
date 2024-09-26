using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updatetoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessTokenGoogle",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6151));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6149));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6147));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6146));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6144));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6142));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6137));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 26, 2, 52, 3, 786, DateTimeKind.Utc).AddTicks(6130));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessTokenGoogle",
                table: "RefreshToken");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5055));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5053));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5051));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5050));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5049));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5048));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5047));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 25, 13, 7, 16, 849, DateTimeKind.Utc).AddTicks(5045));
        }
    }
}
