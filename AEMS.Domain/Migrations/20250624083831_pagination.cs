using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class pagination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c912672-af9d-4837-b2b5-a38fec76b9f3", "AQAAAAIAAYagAAAAEHQ+ci+APinzMRthegrwWsipQsVc4lCkR8lIFYSwjTwn40HuPURyLZQPDokFoWOEHA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "420f802f-9bd4-400f-ac05-b8af96cb5f1f", "AQAAAAIAAYagAAAAEHUUBIsKH1Vu0iRnQb3TY6VRwAaw4dk19j+hj69CvKoTwGlOtZyBOO7Oq0GwvjcNRw==" });
        }
    }
}
