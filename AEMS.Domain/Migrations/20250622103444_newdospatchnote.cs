using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newdospatchnote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vehicle",
                table: "DispatchNotes");

            migrationBuilder.RenameColumn(
                name: "DispatchQty",
                table: "RelatedContract",
                newName: "WidthOrColor");

            migrationBuilder.AddColumn<string>(
                name: "BalanceQuantity",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerRefer",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractQuantity",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DispatchQuantity",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FabricDetails",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rate",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowId",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TotalDispatchQuantity",
                table: "RelatedContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverNumber",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DispatchNotes_TransporterCompanies_TransporterId",
                table: "DispatchNotes");

            migrationBuilder.DropIndex(
                name: "IX_DispatchNotes_TransporterId",
                table: "DispatchNotes");

            migrationBuilder.DropColumn(
                name: "BalanceQuantity",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "BuyerRefer",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "ContractQuantity",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "DispatchQuantity",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "FabricDetails",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "TotalDispatchQuantity",
                table: "RelatedContract");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "DispatchNotes");

            migrationBuilder.DropColumn(
                name: "DriverNumber",
                table: "DispatchNotes");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "DispatchNotes");

            migrationBuilder.RenameColumn(
                name: "WidthOrColor",
                table: "RelatedContract",
                newName: "DispatchQty");

            migrationBuilder.AddColumn<string>(
                name: "Vehicle",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "44be7857-9327-4d39-8c12-b34ce3d4ef45", "AQAAAAIAAYagAAAAEOfMeGoWqUH0G1jAmBYAS40cM+G2lI036971YH6rErSq4dISl21EleLoA/tnW6+h7A==" });
        }
    }
}
