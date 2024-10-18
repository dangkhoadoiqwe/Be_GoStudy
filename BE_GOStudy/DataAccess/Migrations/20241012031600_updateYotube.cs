using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updateYotube : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YoutubeUrl",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3391));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3390));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3389));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3387));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3384));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3383));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3382));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 3, 15, 59, 572, DateTimeKind.Utc).AddTicks(3378));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeUrl",
                table: "Classrooms");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3961));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3960));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3958));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3956));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3955));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3954));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 27, 4, 29, 37, 442, DateTimeKind.Utc).AddTicks(3952));
        }
    }
}
