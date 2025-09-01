using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLENTRYVOCHERUPDATESEVICEFORCALCULATION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e63ef34e-8cfe-4d3d-b994-e7dc71ca7c15", "AQAAAAIAAYagAAAAEPNK90zItJvvgjtUYPy+e694DPpd2fnDnbpNmEBE13vnAQnbUyOx9Pc+lEJkmg4Wfg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eeae579c-8ddb-4456-bb6e-892cfdebc170", "AQAAAAIAAYagAAAAELrzXlnLE81RlVg643zWoJZVYsRnAr3Vi4D/0gGY8J/uiVlp1QtUcqMuA3lRoagyow==" });
        }
    }
}
