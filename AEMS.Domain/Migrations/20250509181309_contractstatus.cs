using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class contractstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4e12b4e2-b485-423c-a75a-b9c1f4032b9a", "AQAAAAIAAYagAAAAELLqoooyaOxIgTZoGxgD5G+g3ilyJzY0iL2LN9HsPhQKZ6CULoi8qVKVFjFuBihsMQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "contracts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fc36ccf-599c-4a3f-8b72-dde9da03e344", "AQAAAAIAAYagAAAAEKAipu0PzME9DCSISUMlSkg4YvmnNBNh9HcAeC4FcOdJK45LpkT+67GpIBrUUF6iKQ==" });
        }
    }
}
