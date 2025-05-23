using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class selvegeess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Selvedge",
                table: "contracts",
                newName: "Selvege");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6272cb8-09e6-45e5-a431-93a6989be1f6", "AQAAAAIAAYagAAAAEHdII/W/HlyMp3iVQpC32t482zyl2AiPExHpvYd9sl40tev+Cjhtsqbuzz7VRlHriA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Selvege",
                table: "contracts",
                newName: "Selvedge");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7266e07d-16f4-4614-9824-09dd5454a2cb", "AQAAAAIAAYagAAAAEJ17hzWADbzuNltnUNboPJDls2X5oz+PjLyJaFix5JG05LPhHKTA/K30Xr4bhx22wg==" });
        }
    }
}
