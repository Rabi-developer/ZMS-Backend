using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class nwd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31c0a69f-71f5-4936-adba-b6191c0c7c5c", "AQAAAAIAAYagAAAAEOUTwXnZckFVGnxN932XLnA4EHpheA0E1iOiw2uHHqJ7cQgMZlTY71E1YPWgHirIxg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e0a23978-3540-46c7-86dd-401f1e972fe4", "AQAAAAIAAYagAAAAEPHYxXWt63O7yI372SS0k7XhBZPT4mkDOqE0mnAc1cA+QszVR6deVuPnVKHqQtLybA==" });
        }
    }
}
