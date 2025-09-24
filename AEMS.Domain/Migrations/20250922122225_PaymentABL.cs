using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class PaymentABL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "267621e2-8822-4a0f-a8a6-95dd5aca1b57", "AQAAAAIAAYagAAAAEMKPfVbjilVzrROnmsEcBy1O1IOf073ccl1NOhquOIdpfhL3WgpM/XXCaipJghkOUQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8e0243c-c8e3-4430-a6f6-5f286e3d7da7", "AQAAAAIAAYagAAAAEMi8n1T3otORccsnQkz2uwpo2xuoaG2xhXMTuFT//V+IzqOpIZFJHeLz1A0LWj0R1A==" });
        }
    }
}
