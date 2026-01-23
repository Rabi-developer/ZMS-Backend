using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class brokeraddcnicandaccountnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Brooker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "Brooker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80aae2ce-a6a1-4f20-964c-28454173a732", "AQAAAAIAAYagAAAAEGpjqE138WtZqoOe0X+MnUeSL+csaB4K3IK/fFsGPRVmidyPYDa6pNtMHYuB18MW5g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Brooker");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "Brooker");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "78e5b756-a871-4645-adc5-aa5a5b2736bc", "AQAAAAIAAYagAAAAELO+Cda0sOAXcwBgFRaQhnGcrOLNJX09Y36+X8jgF+/rsIU2cSQmwsEQSUK8nzAWgQ==" });
        }
    }
}
