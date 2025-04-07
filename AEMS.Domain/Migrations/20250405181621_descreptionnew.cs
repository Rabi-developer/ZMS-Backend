using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class descreptionnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionName",
                table: "Descriptions",
                newName: "Descriptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2a24fc74-056c-48ce-8f1a-b80300060f33", "AQAAAAIAAYagAAAAEGddpcBTYDLAq4O0dA0nm7uwO8gCrRldrUkQ89pZqAfG6gNEtb+Z2sxCwCXh/j360Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptions",
                table: "Descriptions",
                newName: "DescriptionName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc1e3b63-c6e2-4b10-91aa-6f147c81e57c", "AQAAAAIAAYagAAAAEDmhc1oE+pilqzIdWyB7MhF9p1R/Q7QgqB3HqrZIgzID7WWQKRUvNs/SkE9SBVwTIA==" });
        }
    }
}
