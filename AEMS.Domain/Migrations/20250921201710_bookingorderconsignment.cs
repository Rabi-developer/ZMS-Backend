using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class bookingorderconsignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ced2c3d1-f697-4794-bf41-557f8bcb61b0", "AQAAAAIAAYagAAAAECBJktVfE20/fltwGRoxVrYzX9c0XlpXzSdYUQBgvjUa8IceWqV+lDcLimGDgvwN/g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "50ffb932-c885-472d-b762-58149524fcd7", "AQAAAAIAAYagAAAAEEK2eG4Po5D3/Yz3P6axfYeuHxYBi03fbM49V9p5qrp1w4ElSpBMLM3XqqMk7czB5A==" });
        }
    }
}
