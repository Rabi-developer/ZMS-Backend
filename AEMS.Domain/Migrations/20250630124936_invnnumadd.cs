using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class invnnumadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "InspectionNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5e28977-a6bc-4b60-b680-d238b10138dd", "AQAAAAIAAYagAAAAEE1hJeBoOBx54J6X70gyEE6QoQWD48ohO3XTYLZh1jDtkuId7VBeP5fwPsfkaM3htA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "InspectionNotes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9b63a6bc-6f5b-4055-88d1-116dd0274944", "AQAAAAIAAYagAAAAEPVgOyJP+U4IpAKjC8D16OLXibF9/fkc5tN5qBR/BlYq8jA9eILcpvpEZh9DCj66ug==" });
        }
    }
}
