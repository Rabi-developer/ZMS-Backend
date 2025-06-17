using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class h : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0b4a62a3-7d1a-4e93-b022-5ffc94b2d534", "AQAAAAIAAYagAAAAEEvsJoObtWS/fXvrXi+wr/HjyvsI4729dK0dA7/C2cI1otrhQb74fowz5ToRQmmOzA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f9a5f52-ba25-47f5-87e3-9265bb0711f7", "AQAAAAIAAYagAAAAEI4KnhoOABN/00iPDSlgm6RhWg9oQE74tDNcwR5kQW0VGMB3zqk9aL67ikvoYgiEtg==" });
        }
    }
}
