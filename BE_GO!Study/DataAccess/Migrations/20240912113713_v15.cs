using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "PaymentTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MerchantId",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentContent",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentCurrency",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentDestinationId",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentLanguage",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentRefId",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentContent",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentCurrency",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentDestinationId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentLanguage",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentRefId",
                table: "PaymentTransactions");
        }
    }
}
