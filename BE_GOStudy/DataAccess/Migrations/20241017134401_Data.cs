using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "PrivacySettings",
                columns: new[] { "PrivacySettingId", "Visibility" },
                values: new object[,]
                {
                    { 1, "Public" },
                    { 2, "Friends Only" },
                    { 3, "Private" }
                });

            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "SemesterId", "Name" },
                values: new object[,]
                {
                    { 1, "Fall 2024" },
                    { 2, "Spring 2025" },
                    { 3, "Summer 2025" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "SpecializationId", "Name" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Chinese" },
                    { 3, "Japanese" },
                    { 4, "Economics and Management" },
                    { 5, "Marketing" },
                    { 6, "Media and Journalism" },
                    { 7, "Design" },
                    { 8, "Science and Technology" },
                    { 9, "Industry and Construction" },
                    { 10, "General Subjects" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "CreatedAt", "LinkUrl", "Name", "Nickname", "SpecializationId", "YoutubeUrl", "status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9737), "http://example.com/eng101", "Room 101", "Eng101", 1, null, 1 },
                    { 2, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9738), "http://example.com/chi102", "Room 102", "Chi102", 2, null, 1 },
                    { 3, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9739), "http://example.com/jap103", "Room 103", "Jap103", 3, null, 1 },
                    { 4, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9740), "http://example.com/econmgmt104", "Room 104", "EconMgmt104", 4, null, 1 },
                    { 5, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9741), "http://example.com/mkt105", "Room 105", "Mkt105", 5, null, 1 },
                    { 6, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9742), "http://example.com/mediajourn106", "Room 106", "MediaJourn106", 6, null, 1 },
                    { 7, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9742), "http://example.com/design107", "Room 107", "Design107", 7, null, 1 },
                    { 8, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9743), "http://example.com/scitech108", "Room 108", "SciTech108", 8, null, 1 },
                    { 9, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9744), "http://example.com/indconst109", "Room 109", "IndConst109", 9, null, 1 },
                    { 10, new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9745), "http://example.com/gensub110", "Room 110", "GenSub110", 10, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PrivacySettings",
                keyColumn: "PrivacySettingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PrivacySettings",
                keyColumn: "PrivacySettingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrivacySettings",
                keyColumn: "PrivacySettingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "SemesterId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "SemesterId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "SemesterId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 10);

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
                columns: new[] { "ClassroomId", "CreatedAt", "LinkUrl", "Name", "Nickname", "SpecializationId", "YoutubeUrl", "status" },
                values: new object[,]
                {
                    { -10, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2155), "http://example.com/gensub110", "Room 110", "GenSub110", -10, null, 1 },
                    { -9, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2153), "http://example.com/indconst109", "Room 109", "IndConst109", -9, null, 1 },
                    { -8, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2152), "http://example.com/scitech108", "Room 108", "SciTech108", -8, null, 1 },
                    { -7, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2151), "http://example.com/design107", "Room 107", "Design107", -7, null, 1 },
                    { -6, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2104), "http://example.com/mediajourn106", "Room 106", "MediaJourn106", -6, null, 1 },
                    { -5, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2103), "http://example.com/mkt105", "Room 105", "Mkt105", -5, null, 1 },
                    { -4, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2102), "http://example.com/econmgmt104", "Room 104", "EconMgmt104", -4, null, 1 },
                    { -3, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2100), "http://example.com/jap103", "Room 103", "Jap103", -3, null, 1 },
                    { -2, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2099), "http://example.com/chi102", "Room 102", "Chi102", -2, null, 1 },
                    { -1, new DateTime(2024, 10, 12, 5, 20, 19, 96, DateTimeKind.Utc).AddTicks(2097), "http://example.com/eng101", "Room 101", "Eng101", -1, null, 1 }
                });
        }
    }
}
