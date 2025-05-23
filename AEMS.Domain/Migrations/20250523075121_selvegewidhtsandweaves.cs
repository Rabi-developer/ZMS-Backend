using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class selvegewidhtsandweaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelvedgeWidth",
                table: "contracts",
                newName: "SelvegeWidth");

            migrationBuilder.RenameColumn(
                name: "SelvedgeWeave",
                table: "contracts",
                newName: "SelvegeWeaves");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d8041b2-c1b1-40a7-997f-435fdf92d9fb", "AQAAAAIAAYagAAAAEMiChho/W1vM1V2CfA98nDKHltvBCrU5FW3aUxvVVY2wmYj8+h5vppH8utMIDOrw5g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelvegeWidth",
                table: "contracts",
                newName: "SelvedgeWidth");

            migrationBuilder.RenameColumn(
                name: "SelvegeWeaves",
                table: "contracts",
                newName: "SelvedgeWeave");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6272cb8-09e6-45e5-a431-93a6989be1f6", "AQAAAAIAAYagAAAAEHdII/W/HlyMp3iVQpC32t482zyl2AiPExHpvYd9sl40tev+Cjhtsqbuzz7VRlHriA==" });
        }
    }
}
