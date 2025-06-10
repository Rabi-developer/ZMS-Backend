using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class descriptionss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DescriptionId",
                table: "contracts",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d1eb9adf-4062-4e61-b547-c79634652786", "AQAAAAIAAYagAAAAEEtaU1gEhobRaYTsKw3mZssMha7Dbj3CKwfABg8XtOAMqIkpnRzziBk8HkMIoaTgMQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "contracts",
                newName: "DescriptionId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2f32f8d1-cd09-4df6-9c6b-4852c7490712", "AQAAAAIAAYagAAAAEPKIltGtuAk0gkSmArwYslNH2xQSLp7pxr2kfErtA3IqPXwfOpTWhc6DQ4gmaMk5rg==" });
        }
    }
}
