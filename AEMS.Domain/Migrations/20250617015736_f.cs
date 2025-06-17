using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "SampleDetail");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5dd3ea5a-b844-49c9-a5a7-75e905168abf", "AQAAAAIAAYagAAAAEL0LjHzLS4j+L77fAkV7O/en7k7qaYC3fi4XZTJC0Pdm1M+aHGFGINXqG+qtAnDpVA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDeliveredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleReceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleDetail_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Labs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YarnBags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                        column: x => x.SampleDetailId,
                        principalTable: "SampleDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0098a8f3-1197-49f5-b3dd-d42dca62c2e7", "AQAAAAIAAYagAAAAEGmpmaxufPN1k82pmQEyCdavSNjlJ8kMfcw0cVefDI4QlCPAfoIePJmbMDHGfx750w==" });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfo_SampleDetailId",
                table: "AdditionalInfo",
                column: "SampleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleDetail_ContractId",
                table: "SampleDetail",
                column: "ContractId");
        }
    }
}
