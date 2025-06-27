using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class inspectionlistidremovetoDispatchNoteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Listid",
                table: "InspectionNotes",
                newName: "DispatchNoteId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a18e5134-58c0-49ce-aa42-268be1850edc", "AQAAAAIAAYagAAAAEKt9LPdpGmfGo3tg//+365NuAz8raMfoqkXpAVmjKwf5HbxPyTLv7L364QKagYhqoQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DispatchNoteId",
                table: "InspectionNotes",
                newName: "Listid");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b86af3b-138e-4f77-8c27-83ccb84a5815", "AQAAAAIAAYagAAAAEBoeily0fVitMmSFjt4QeOEQYv4WjHgyRYpAp9lnYXBpP1MjjlixSsN4vmN9CWw4CA==" });
        }
    }
}
