using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class billpaymnetchargeno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChargeNo",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3beda23f-a969-485d-9f20-afb1530a88ed", "AQAAAAIAAYagAAAAEER/rO3mzaMMSgQo3DkQKw0y3aFypgs13xuyd8gkcBky1uZdnLU8wi52796SFsKEXA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeNo",
                table: "BiltyPaymentInvoiceLine");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "324f61ca-440b-47eb-935d-7392b32523b6", "AQAAAAIAAYagAAAAEIfYE8AXbZ+l1lNGXXRg+RSVnnLQDOnpyf5VdGZeY0a5luXiyLcdCUnXLNWwmsUFkg==" });
        }
    }
}
