using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newdispatchnotelistid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Listid",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d0da466-c002-4618-a04d-55dd5bac1f0d", "AQAAAAIAAYagAAAAEIAzqV5ACNAXMbOVtO+MESmjGaI5Bsp3i+9hHeiecV63hMtlQCrY6DXv/6fFMawKFQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Listid",
                table: "DispatchNotes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "14536ce2-2c56-46ab-9443-e0c9b0e8918d", "AQAAAAIAAYagAAAAEPy2PN8XX+Ojx/fi3h5INM0/vXOog+5bU2Q02DszG7cBIoBU2EdVza14D4fWj0JXJw==" });
        }
    }
}
