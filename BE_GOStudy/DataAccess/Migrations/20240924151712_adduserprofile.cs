using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class adduserprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birthday",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9338));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9337));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9335));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9334));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9333));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9329));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 24, 15, 17, 12, 138, DateTimeKind.Utc).AddTicks(9324));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthday",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "Users");

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
        }
    }
}
