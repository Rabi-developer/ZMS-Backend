using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dhsaneadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "MultiWidthContractRow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a97ccb01-ec8e-4dc5-9928-4d11e4b49907", "AQAAAAIAAYagAAAAEECFaev4TBfs2x7vEMkhBJ9gsJqcuvfcaTXP5to3ChA7RRFuBTv+MKW3UDzLXZh7bQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MultiWidthContractRow");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2dfc330d-fc89-4bce-8b34-39e5f25e54ce", "AQAAAAIAAYagAAAAEDh9Cz1qAJO86TIsK4rQd/nAhMpYoav7BncD1mKGUEWyNeWIKy3i60+tkXecrDdmXw==" });
        }
    }
}
