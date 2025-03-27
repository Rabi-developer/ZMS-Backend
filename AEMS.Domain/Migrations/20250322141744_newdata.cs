using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5092de01-86bd-4fa5-b8bd-a5283d852bf0", "AQAAAAIAAYagAAAAEBtHiitVO560VbfzDN8/5tXnGuEu9EcZSJl7A1U+/l9h9lznG+yeMl4CAn2SixMowA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8b3681e-4440-43a4-bb8f-e5cfa862f5ef", "AQAAAAIAAYagAAAAEKE6pvKm1+FRbr1dFEW6A368hkjXlWtogPICmC14yFEyjkVgJ9i6b7d4rL4HfVPxgQ==" });
        }
    }
}
