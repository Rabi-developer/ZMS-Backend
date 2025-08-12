using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class acoubtidgenerate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "758f79bd-e9d7-44e1-87cc-67198264b9ff", "AQAAAAIAAYagAAAAEMsTdrVJqITzBbxYrDmBS0JYolhH4lc5x1NIp1L+p5Z1MJz/TzdCQfHcn2MFZID2Xw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba61e679-e029-4870-be4a-1c69ba5ffa9d", "AQAAAAIAAYagAAAAEJAqNT0vPhIX47kwIqhGJJwahNo5O3FrLzdVC2dDxwaek1/LEDE/s5MvrTWXJpl76Q==" });
        }
    }
}
