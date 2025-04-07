using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class descriptionnewagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "Descriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d782d49c-770b-48c8-b2ef-383d27770218", "AQAAAAIAAYagAAAAENz/+fS7gUZLZOsh5a8vFgJuskLGiNPftg7pCOeDLBPmXn+NcuOAs73mXSJHnSYW9Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "Descriptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d108cc08-05f4-4ce6-9118-e2727e931d0b", "AQAAAAIAAYagAAAAEMFZpC6fR+sdiMU9vpkxvIFUwtB7+mdzKHPhZC5Tt++3Eq3qSOH2pxTdYUMoemkl4w==" });
        }
    }
}
