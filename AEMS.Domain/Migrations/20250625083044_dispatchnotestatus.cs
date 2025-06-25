using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dispatchnotestatus : Migration
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
                values: new object[] { "0ab825a7-b58c-4d74-825b-808e7935293f", "AQAAAAIAAYagAAAAELSVhmoNwoOUXDGE5FsBb84yj3PtXrkzBDSO7TGBXjN9xm3PY2NQVK45i8PhAfgVHQ==" });
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
