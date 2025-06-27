using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newinspectionnotechanging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "InspectionNotes",
                newName: "Listid");

            migrationBuilder.AddColumn<string>(
                name: "ReturnFabric",
                table: "InspectionContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shrinkage",
                table: "InspectionContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b86af3b-138e-4f77-8c27-83ccb84a5815", "AQAAAAIAAYagAAAAEBoeily0fVitMmSFjt4QeOEQYv4WjHgyRYpAp9lnYXBpP1MjjlixSsN4vmN9CWw4CA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnFabric",
                table: "InspectionContract");

            migrationBuilder.DropColumn(
                name: "Shrinkage",
                table: "InspectionContract");

            migrationBuilder.RenameColumn(
                name: "Listid",
                table: "InspectionNotes",
                newName: "InvoiceNumber");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dcc5dba4-8bbb-48f9-86f1-9877b12cec5a", "AQAAAAIAAYagAAAAEJz8TbxykU99t0V7knbg+NH1siY0gKK/xRS5l89tnZ9hP5JBSnO8fFo9VLnP8b4kvw==" });
        }
    }
}
