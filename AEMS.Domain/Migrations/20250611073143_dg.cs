using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e747d38-682d-41c9-96ab-3ecc5d0abe2f", "AQAAAAIAAYagAAAAEMIGamkrGyCQ+neM69KuRI6vw5c1Bb/WxdsS9ahbAF8OYNW4QsOc/V5b13WwtXKd4A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "758ad441-961c-47df-a276-17d78344cff0", "AQAAAAIAAYagAAAAELSzCWpBfKF+gr6oKa2JOwZ1MzKtzxK4BUX8/HalwbA0P1ACCnlAUaYlH4KnsxWEvA==" });
        }
    }
}
