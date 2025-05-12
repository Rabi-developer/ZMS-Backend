using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dispatchnote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DispatchNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bilty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatedContract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedContract_DispatchNotes_DispatchNoteId",
                        column: x => x.DispatchNoteId,
                        principalTable: "DispatchNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7266e07d-16f4-4614-9824-09dd5454a2cb", "AQAAAAIAAYagAAAAEJ17hzWADbzuNltnUNboPJDls2X5oz+PjLyJaFix5JG05LPhHKTA/K30Xr4bhx22wg==" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedContract_DispatchNoteId",
                table: "RelatedContract",
                column: "DispatchNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedContract");

            migrationBuilder.DropTable(
                name: "DispatchNotes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4e12b4e2-b485-423c-a75a-b9c1f4032b9a", "AQAAAAIAAYagAAAAELLqoooyaOxIgTZoGxgD5G+g3ilyJzY0iL2LN9HsPhQKZ6CULoi8qVKVFjFuBihsMQ==" });
        }
    }
}
