using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ckhshvc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "08ab247a-f40a-485b-8a24-3cbc61cb9634", "AQAAAAIAAYagAAAAEPQUnF2Z2El7MFm4t8HnLs8kw/qOkEpk8TUPPnR6fKC3Wo49y8ms4F6em6zw0ofCbA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f823eacf-33a3-4cf4-a1af-0e77a79d88bf", "AQAAAAIAAYagAAAAEF1VJjOApHLAqynoI1J5bbrV3CkF5rzpvUilkEFNZlZiyOgdF4CcNfHNizExl9KCUg==" });
        }
    }
}
