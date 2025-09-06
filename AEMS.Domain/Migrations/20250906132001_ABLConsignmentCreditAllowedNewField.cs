using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLConsignmentCreditAllowedNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditAllowed",
                table: "Consignment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "309d5d33-ee46-495e-99be-3c6f6dbea017", "AQAAAAIAAYagAAAAEI+AgdPovmoX9UgIVsHgx69Vn+Qtv4WLh7MY35kQRj7N4+LC/SWbl75bCnbRH5mC+w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditAllowed",
                table: "Consignment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e63ef34e-8cfe-4d3d-b994-e7dc71ca7c15", "AQAAAAIAAYagAAAAEPNK90zItJvvgjtUYPy+e694DPpd2fnDnbpNmEBE13vnAQnbUyOx9Pc+lEJkmg4Wfg==" });
        }
    }
}
