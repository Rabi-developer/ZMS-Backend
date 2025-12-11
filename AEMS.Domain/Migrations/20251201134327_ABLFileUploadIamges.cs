using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLFileUploadIamges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Files",
                table: "RelatedConsignments");

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "BookingOrder",
                type: "nvarchar(max)",
                nullable: true);    

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2cc982c9-13d2-48a5-85a9-127fa2afd240", "AQAAAAIAAYagAAAAEAeYPJfd9Gh8Y9J1Q56Yuumk67NNQXncIUBsJ8iIMXs9UdWOp6BsR/XOzkHWseG2yQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Files",
                table: "BookingOrder");

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "RelatedConsignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2c38589-6cf2-4d26-b7ee-c531859795c3", "AQAAAAIAAYagAAAAEDIXjI+eCRQEQhMgLz9lSoE7+6Xr17vjS1TQ/rr5SwgfFs5Q3F0IlkF0a6vyBfFASA==" });
        }
    }
}
