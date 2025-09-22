using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class paginaitonsize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8e0243c-c8e3-4430-a6f6-5f286e3d7da7", "AQAAAAIAAYagAAAAEMi8n1T3otORccsnQkz2uwpo2xuoaG2xhXMTuFT//V+IzqOpIZFJHeLz1A0LWj0R1A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6e0a48a2-e928-41f8-902c-eb7961e599a9", "AQAAAAIAAYagAAAAEBcGzbYchpwokW7OQz3Eif4mBnLju1RpkOq2DEB1Au/evLPo9OH3WH2NfAdUopqppg==" });
        }
    }
}
