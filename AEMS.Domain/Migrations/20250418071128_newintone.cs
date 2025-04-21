using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newintone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e94d84b-5151-4506-807d-74d7c92833f2", "AQAAAAIAAYagAAAAEPl6ukIisaa7rUbp6AzBfSRWt0ikoe+4K22XopmN3QFhgvEvVCULgb/seZZorMFlYg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "110d3f1f-41f3-4dd3-b588-810b98633d23", "AQAAAAIAAYagAAAAEFesy6qkjIIx59j/lhc0jHP0is/1AKrnn7R0aa9MfyTynFF8rpa+FSjOQ6U8bSxnaA==" });
        }
    }
}
