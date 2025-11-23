using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderProgress_Fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f218215-5132-43e8-b44e-ba51dfc6e313", "AQAAAAIAAYagAAAAEDOacZLbuv8UCwfngybp0s7aYgq+nA8tjnCqxUVdiDlUsBAHcQINTPxH1AHoZ8mRZA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7c71d9f4-4990-49c6-9855-9ad90e14f7cf", "AQAAAAIAAYagAAAAEMZkt8TLj96iOJooIERR8KbzkICcn+tRUVodLjKvc0oWgPgNq/m964oMxsbLwRHKiw==" });
        }
    }
}
