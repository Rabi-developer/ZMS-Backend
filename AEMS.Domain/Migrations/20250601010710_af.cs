using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class af : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Listid",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "68a2f42f-41fa-45e3-a0c0-dbcf3b4c9de9", "AQAAAAIAAYagAAAAEI9v9M5KU/mtc7cEG2GOLUPN++0mrN9Y0jYyvCNHxNm1JG29HNhOjrzMewTxrrQdrA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Listid",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d0da466-c002-4618-a04d-55dd5bac1f0d", "AQAAAAIAAYagAAAAEIAzqV5ACNAXMbOVtO+MESmjGaI5Bsp3i+9hHeiecV63hMtlQCrY6DXv/6fFMawKFQ==" });
        }
    }
}
