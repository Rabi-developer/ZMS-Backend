using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLEntryVoucherServiceNewFunctionandControlleradd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eeae579c-8ddb-4456-bb6e-892cfdebc170", "AQAAAAIAAYagAAAAELrzXlnLE81RlVg643zWoJZVYsRnAr3Vi4D/0gGY8J/uiVlp1QtUcqMuA3lRoagyow==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "868c5f64-e927-4590-b333-733e807431b2", "AQAAAAIAAYagAAAAEIKN4wIY9GqAHYOJSOqfL0He21Yg7o/x/IUs6rG53sY/QQSVLXo98m6keW+2iIKdAA==" });
        }
    }
}
