using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLBookingorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "50ffb932-c885-472d-b762-58149524fcd7", "AQAAAAIAAYagAAAAEEK2eG4Po5D3/Yz3P6axfYeuHxYBi03fbM49V9p5qrp1w4ElSpBMLM3XqqMk7czB5A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35a56879-5a52-4871-959e-05aea3928d3c", "AQAAAAIAAYagAAAAEKK4fCO32jsgp3JNXK6gqGZy3R7vzvvfLxV9NC2QTi8crrtC4TVcSBvBuWT7cDGNQg==" });
        }
    }
}
