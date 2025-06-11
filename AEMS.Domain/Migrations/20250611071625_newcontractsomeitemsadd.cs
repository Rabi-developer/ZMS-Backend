using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newcontractsomeitemsadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gsm",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InductionThread",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelvegeThickness",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "758ad441-961c-47df-a276-17d78344cff0", "AQAAAAIAAYagAAAAELSzCWpBfKF+gr6oKa2JOwZ1MzKtzxK4BUX8/HalwbA0P1ACCnlAUaYlH4KnsxWEvA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gsm",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "InductionThread",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "SelvegeThickness",
                table: "contracts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8894094-fe81-46f0-a8fa-5973e58dd03d", "AQAAAAIAAYagAAAAEL+Yh9IL3/ESrfFue8p3xY+N4UbD/tINncrzmyf7789Oog9N/7NUs2N2MUv6caBw2A==" });
        }
    }
}
