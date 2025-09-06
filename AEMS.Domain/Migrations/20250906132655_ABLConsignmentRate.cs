using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLConsignmentRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "ConsignmentItem",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c93cf7ba-e6cb-4995-a375-dc4b75635fbe", "AQAAAAIAAYagAAAAEOEG5WVk8OIoWess3wSWNgl9wCuzA73yMp1nP7TmbuoW4Xuuhyih2MRcGCuHQXWOaQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "ConsignmentItem");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "309d5d33-ee46-495e-99be-3c6f6dbea017", "AQAAAAIAAYagAAAAEI+AgdPovmoX9UgIVsHgx69Vn+Qtv4WLh7MY35kQRj7N4+LC/SWbl75bCnbRH5mC+w==" });
        }
    }
}
