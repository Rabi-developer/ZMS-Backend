using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLPaymentABLmunshayanastringontodecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Munshayana",
                table: "BiltyPaymentInvoiceLine",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a5871dc-e5b7-48b7-8970-3243b37a492d", "AQAAAAIAAYagAAAAEGoQw//kciBCzYq5uOX+6gMmeNkUzH62hj0RcPK9+oiVLal65OpMgmoL2fNIENJpgA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Munshayana",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "87c37524-7f43-4ed9-a9f1-a2563966057a", "AQAAAAIAAYagAAAAEOsrJuC7/sAOu2JdZv4yQpNpUk5l1pcAopj47ZPiJuoIbjbJizwHD8Mc0nAPdyDUJw==" });
        }
    }
}
