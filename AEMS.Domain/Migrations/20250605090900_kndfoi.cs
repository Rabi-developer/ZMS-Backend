using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class kndfoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b67bf435-7090-4a9b-b9b3-2d477c1f8efa", "AQAAAAIAAYagAAAAED4KE93din838U18gUJ+jDrShM2/WRbxzJHYadPirWwEfssmegAMYw9FJjFedWGR3Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4cd83f92-1138-4217-80b4-e035be8cb21f", "AQAAAAIAAYagAAAAEPYR2UmoJLhGq4HUbJxgPCDhNZ2a02FXxVlG+uQCriXvMCzwgZ/iPYG7Ys7LrQfqoA==" });
        }
    }
}
