using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class news : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "be9283f1-8d4f-4bf5-979e-98e27875a393", "AQAAAAIAAYagAAAAECCPgl4fGW7FRgIVNrolGzw+/ULQM5XGOkOA2Nv3fR6BbWWBt08YaNIKq/gQGIeEMw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d782d49c-770b-48c8-b2ef-383d27770218", "AQAAAAIAAYagAAAAENz/+fS7gUZLZOsh5a8vFgJuskLGiNPftg7pCOeDLBPmXn+NcuOAs73mXSJHnSYW9Q==" });
        }
    }
}
