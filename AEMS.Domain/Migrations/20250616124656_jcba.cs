using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class jcba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f02c19f-a14d-41d1-a0fd-336e2902e2f4", "AQAAAAIAAYagAAAAEAzxjenO76krgO4T7Pjn9Q6spSJ0uKuu7uD0IpBRhK4A/wONRfw/Bgeb4SrzslbtIw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a45f62a1-cbcd-422f-b8ed-d5f53b5a522b", "AQAAAAIAAYagAAAAEHSuHhl3qnth4JofdsqilVczssg1r3kF6cjCXnEOZsX9vrraxNhjCHZSMj7Hqwj0bA==" });
        }
    }
}
