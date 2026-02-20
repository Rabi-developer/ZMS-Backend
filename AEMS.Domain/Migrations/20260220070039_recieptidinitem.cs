using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class recieptidinitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b710636e-5509-431d-99e5-ed1a5ba6b9ae", "AQAAAAIAAYagAAAAEBI++NOULh0okk1oaocIAH9A9K2kaXQbPWpuwihGU9F0BR14b0XXAIJ1xWcGycj3vw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c1a28901-2b7b-49a2-8356-0007effedb10", "AQAAAAIAAYagAAAAEHVoLDcULYx7VlHAQK5GUS3anvUsag+/ZF2TGK7KQ+ZTjgKUOB54qtguJiBsVslbAg==" });
        }
    }
}
