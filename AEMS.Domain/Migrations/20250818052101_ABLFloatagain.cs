using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLFloatagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PaidAmount",
                table: "ChargesPayments",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f823eacf-33a3-4cf4-a1af-0e77a79d88bf", "AQAAAAIAAYagAAAAEF1VJjOApHLAqynoI1J5bbrV3CkF5rzpvUilkEFNZlZiyOgdF4CcNfHNizExl9KCUg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaidAmount",
                table: "ChargesPayments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba35c988-149b-4495-8b30-a2df3f96fbff", "AQAAAAIAAYagAAAAEJFrbxvDqkmYzf0i8ximA8GScEcdx29G3isSgNmFbaICiAjZb1XERTgjMpoCR/M4fg==" });
        }
    }
}
