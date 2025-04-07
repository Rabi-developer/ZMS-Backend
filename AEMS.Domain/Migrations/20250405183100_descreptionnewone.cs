using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class descreptionnewone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "2a24fc74-056c-48ce-8f1a-b80300060f33", "AQAAAAIAAYagAAAAEGddpcBTYDLAq4O0dA0nm7uwO8gCrRldrUkQ89pZqAfG6gNEtb+Z2sxCwCXh/j360Q==" });
        }
    }
}
