using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class insppectionnotess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InspectionNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IrnNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IrnDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Buyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InspectionContract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispatchQty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionContract_InspectionNotes_InspectionNoteId",
                        column: x => x.InspectionNoteId,
                        principalTable: "InspectionNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4cd83f92-1138-4217-80b4-e035be8cb21f", "AQAAAAIAAYagAAAAEPYR2UmoJLhGq4HUbJxgPCDhNZ2a02FXxVlG+uQCriXvMCzwgZ/iPYG7Ys7LrQfqoA==" });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionContract_InspectionNoteId",
                table: "InspectionContract",
                column: "InspectionNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionContract");

            migrationBuilder.DropTable(
                name: "InspectionNotes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31c0a69f-71f5-4936-adba-b6191c0c7c5c", "AQAAAAIAAYagAAAAEOUTwXnZckFVGnxN932XLnA4EHpheA0E1iOiw2uHHqJ7cQgMZlTY71E1YPWgHirIxg==" });
        }
    }
}
