using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "SpecializationId", "Name" },
                values: new object[,]
                {
                    { -10, "General Subjects" },
                    { -9, "Industry and Construction" },
                    { -8, "Science and Technology" },
                    { -7, "Design" },
                    { -6, "Media and Journalism" },
                    { -5, "Marketing" },
                    { -4, "Economics and Management" },
                    { -3, "Japanese" },
                    { -2, "Chinese" },
                    { -1, "English" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "CreatedAt", "Name", "Nickname", "SpecializationId" },
                values: new object[,]
                {
                    { -10, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6701), "Room 110", "GenSub110", -10 },
                    { -9, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6700), "Room 109", "IndConst109", -9 },
                    { -8, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6699), "Room 108", "SciTech108", -8 },
                    { -7, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6698), "Room 107", "Design107", -7 },
                    { -6, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6697), "Room 106", "MediaJourn106", -6 },
                    { -5, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6696), "Room 105", "Mkt105", -5 },
                    { -4, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6695), "Room 104", "EconMgmt104", -4 },
                    { -3, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6694), "Room 103", "Jap103", -3 },
                    { -2, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6693), "Room 102", "Chi102", -2 },
                    { -1, new DateTime(2024, 9, 16, 17, 0, 56, 921, DateTimeKind.Utc).AddTicks(6692), "Room 101", "Eng101", -1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: -1);
        }
    }
}
