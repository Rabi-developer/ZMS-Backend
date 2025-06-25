using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class nahfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dcc5dba4-8bbb-48f9-86f1-9877b12cec5a", "AQAAAAIAAYagAAAAEJz8TbxykU99t0V7knbg+NH1siY0gKK/xRS5l89tnZ9hP5JBSnO8fFo9VLnP8b4kvw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DispatchNotes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bb609df0-a740-4704-ada0-f967df5fb104", "AQAAAAIAAYagAAAAEPSAzRLJrAeopCgKnhaQNLf65hGJBRwiJTF8BwosrCZ2Yu2Pxv2lbAW6OHYCqIhS8A==" });
        }
    }
}
