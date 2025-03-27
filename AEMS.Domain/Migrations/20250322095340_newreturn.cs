using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newreturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9174efa-b575-426c-b682-163b8732782d", "AQAAAAIAAYagAAAAEK83hJXSPM6GqjZQe54FaUUdYAYmPJ5AJjYNlfMs/LtrFRlZ+onda23+VXsOootEew==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8549828c-3311-434c-bb15-e8a61ffeaa4f", "AQAAAAIAAYagAAAAEA1RtkiuQovVr6gGAT8J4jXExqS2dgPtbL8sWgBvo9+2TwRGN0usNkVE7GDouSSjdw==" });
        }
    }
}
