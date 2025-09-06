using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLPaymentABLNewFieldadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Advanced",
                table: "PaymentABL",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdvancedDate",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PDC",
                table: "PaymentABL",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PDCDate",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PaymentAmount",
                table: "PaymentABL",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35a56879-5a52-4871-959e-05aea3928d3c", "AQAAAAIAAYagAAAAEKK4fCO32jsgp3JNXK6gqGZy3R7vzvvfLxV9NC2QTi8crrtC4TVcSBvBuWT7cDGNQg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Advanced",
                table: "PaymentABL");

            migrationBuilder.DropColumn(
                name: "AdvancedDate",
                table: "PaymentABL");

            migrationBuilder.DropColumn(
                name: "PDC",
                table: "PaymentABL");

            migrationBuilder.DropColumn(
                name: "PDCDate",
                table: "PaymentABL");

            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "PaymentABL");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c93cf7ba-e6cb-4995-a375-dc4b75635fbe", "AQAAAAIAAYagAAAAEOEG5WVk8OIoWess3wSWNgl9wCuzA73yMp1nP7TmbuoW4Xuuhyih2MRcGCuHQXWOaQ==" });
        }
    }
}
