using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class adddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Free", 0.00m },
                    { 2, "Plus", 39000.00m },
                    { 3, "Premium", 59000.00m }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "FeatureId", "Name", "PackageId" },
                values: new object[,]
                {
                    { 4, "Shows a total of 2 rooms corresponding to 2 subjects", 1 },
                    { 5, "Join the room of 2 subjects, after 3 days of use, you have the right to reset the room ~ subject", 1 },
                    { 6, "Provide symbolic times to be able to enhance the study schedule for subjects", 1 },
                    { 7, "Posts in the community are archived, but there are limits", 1 },
                    { 8, "Chat and exchange with friends in your community", 1 },
                    { 9, "Graded learning ability by week and by semester", 1 },
                    { 10, "Unlock 4 rooms corresponding to 4 subjects", 2 },
                    { 11, "Join the room of 4 subjects, after 1 day of use, you have the right to reset the room ~ subject", 2 },
                    { 12, "Provide a timetable to be able to schedule classes for subjects", 2 },
                    { 13, "The calendar will pop up in the room to fill in the next lesson", 2 },
                    { 14, "Store posts in a comfortable community", 2 },
                    { 15, "Chat and exchange with friends in your community", 2 },
                    { 16, "Evaluated and ranked learning productivity by day, week and semester", 2 },
                    { 17, "Light/Dark interface of Study Room", 2 },
                    { 18, "Unlock 6 rooms corresponding to 6 subjects", 3 },
                    { 19, "Join the room of 6 subjects, after 2 hours of use, you have the right to reset the room ~ subject", 3 },
                    { 20, "Provide a timetable to be able to schedule classes for subjects", 3 },
                    { 21, "Take notes and save them during the learning process", 3 },
                    { 22, "Store posts in a comfortable community", 3 },
                    { 23, "The calendar will pop up in the room to fill in the next lesson", 3 },
                    { 24, "Chat and exchange with friends in your community", 3 },
                    { 25, "Evaluated and ranked learning productivity by day, week and semester", 3 },
                    { 26, "Light/Dark interface of Study Room", 3 },
                    { 27, "Exclusive 30-day Premium avatar frame interface helps you stand out", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "FeatureId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "PackageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "PackageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "PackageId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9738));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9739));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9741));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9742));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9742));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9744));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 13, 44, 0, 681, DateTimeKind.Utc).AddTicks(9745));
        }
    }
}
