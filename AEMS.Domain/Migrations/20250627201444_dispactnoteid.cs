using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dispactnoteid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9b63a6bc-6f5b-4055-88d1-116dd0274944", "AQAAAAIAAYagAAAAEPVgOyJP+U4IpAKjC8D16OLXibF9/fkc5tN5qBR/BlYq8jA9eILcpvpEZh9DCj66ug==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a18e5134-58c0-49ce-aa42-268be1850edc", "AQAAAAIAAYagAAAAEKt9LPdpGmfGo3tg//+365NuAz8raMfoqkXpAVmjKwf5HbxPyTLv7L364QKagYhqoQ==" });
        }
    }
}
