using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class idsremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa5706b2-8ad6-4792-bd36-f789304bff38", "AQAAAAIAAYagAAAAEI36PXc+X0AtafzG7TurS9Fm2S1sJOoPZ4csj4c3shlPZNTlIZOnP0k8liuXMC2h6A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a4aa14bb-d0e8-4fb4-baf9-a1632f0de29e", "AQAAAAIAAYagAAAAEB/9pOCz8aU2PY/fqJNmIvVi/AZDjLgTVyVlpLB5RXhSnkzCO2nqatGG/VtHZV2W/w==" });
        }
    }
}
