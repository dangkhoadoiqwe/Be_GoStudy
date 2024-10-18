using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class NewFieldContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Detail",
                table: "ContactInfos",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ContactType",
                table: "ContactInfos",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadedFilePath",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 18, 6, 44, 45, 191, DateTimeKind.Utc).AddTicks(5370));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "UploadedFilePath",
                table: "ContactInfos");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ContactInfos",
                newName: "Detail");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ContactInfos",
                newName: "ContactType");

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5413));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5419));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 55, 39, 45, DateTimeKind.Utc).AddTicks(5422));
        }
    }
}
