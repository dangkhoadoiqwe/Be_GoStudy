using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddUrlClass : Migration
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
                columns: new[] { "ClassroomId", "CreatedAt", "LinkUrl", "Name", "Nickname", "SpecializationId", "status" },
                values: new object[,]
                {
                    { -10, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4960), "http://example.com/gensub110", "Room 110", "GenSub110", -10, 1 },
                    { -9, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4959), "http://example.com/indconst109", "Room 109", "IndConst109", -9, 1 },
                    { -8, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4957), "http://example.com/scitech108", "Room 108", "SciTech108", -8, 1 },
                    { -7, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4956), "http://example.com/design107", "Room 107", "Design107", -7, 1 },
                    { -6, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4955), "http://example.com/mediajourn106", "Room 106", "MediaJourn106", -6, 1 },
                    { -5, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4953), "http://example.com/mkt105", "Room 105", "Mkt105", -5, 1 },
                    { -4, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4952), "http://example.com/econmgmt104", "Room 104", "EconMgmt104", -4, 1 },
                    { -3, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4951), "http://example.com/jap103", "Room 103", "Jap103", -3, 1 },
                    { -2, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4950), "http://example.com/chi102", "Room 102", "Chi102", -2, 1 },
                    { -1, new DateTime(2024, 9, 17, 3, 27, 13, 849, DateTimeKind.Utc).AddTicks(4948), "http://example.com/eng101", "Room 101", "Eng101", -1, 1 }
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
