using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DispatchNotes_TransporterCompanies_TransporterId",
                table: "DispatchNotes");

            migrationBuilder.DropIndex(
                name: "IX_DispatchNotes_TransporterId",
                table: "DispatchNotes");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "DispatchNotes");

            migrationBuilder.AddColumn<string>(
                name: "Transporter",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f377836b-100a-4986-852f-8d556b02f0a2", "AQAAAAIAAYagAAAAEL1vwuEKa6vueJugnPFCtj35NShbtLYLO36kS72+Zr2yJ3g78NQSEyFy0gp1riknjQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transporter",
                table: "DispatchNotes");

            migrationBuilder.AddColumn<Guid>(
                name: "TransporterId",
                table: "DispatchNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8bf35947-7b0f-48a6-8523-ebfb63d7ca9f", "AQAAAAIAAYagAAAAED7y4XALbpLVxtmhsbxv6ruSS+Jy7cZ92zz0BRZA4sfhK0yG4cgHbfnqgtvnMjOfOA==" });

            migrationBuilder.CreateIndex(
                name: "IX_DispatchNotes_TransporterId",
                table: "DispatchNotes",
                column: "TransporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DispatchNotes_TransporterCompanies_TransporterId",
                table: "DispatchNotes",
                column: "TransporterId",
                principalTable: "TransporterCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
