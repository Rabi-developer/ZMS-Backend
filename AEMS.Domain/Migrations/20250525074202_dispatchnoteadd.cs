using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dispatchnoteadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DispatchQty",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa33a961-3dcf-4a3d-9161-345f9c9a6e76", "AQAAAAIAAYagAAAAECZpVQ8pMDaujHt/AiD6tqsMMmcldafe9+5lAyY4hzDKN3RrnUzEXQmFQM1591kocg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "DispatchQty",
                table: "RelatedContract");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d8041b2-c1b1-40a7-997f-435fdf92d9fb", "AQAAAAIAAYagAAAAEMiChho/W1vM1V2CfA98nDKHltvBCrU5FW3aUxvVVY2wmYj8+h5vppH8utMIDOrw5g==" });
        }
    }
}
