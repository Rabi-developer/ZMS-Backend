using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dhsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2dfc330d-fc89-4bce-8b34-39e5f25e54ce", "AQAAAAIAAYagAAAAEDh9Cz1qAJO86TIsK4rQd/nAhMpYoav7BncD1mKGUEWyNeWIKy3i60+tkXecrDdmXw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a54eada8-d56f-473c-b586-413507da179e", "AQAAAAIAAYagAAAAEO5/ZUP6Q1NMUtsXhhJuLh/xLz6wRnlhDAocVbmcjtRjDaAavLJijsLEC4Bt8XY4dg==" });
        }
    }
}
