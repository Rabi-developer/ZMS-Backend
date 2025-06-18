using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "060b0dcf-1d68-496f-9eaf-1b6187efb45c", "AQAAAAIAAYagAAAAEDretBJx4CAOx1ZcBfR/8eVjn4tbCjwanC8maz8hDypZ8go7foiCj1xKlH2Wj8jHOQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a766d57a-0183-45bb-9d75-a0e2eb6cabc4", "AQAAAAIAAYagAAAAEGmWGYtdwPxX88yX3+0K8LzAMr28XmCdx5hMUB7YOyfiFwoAKdGW1CCAPGlb5Juqmg==" });
        }
    }
}
