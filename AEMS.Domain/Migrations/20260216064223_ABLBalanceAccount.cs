using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLBalanceAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountOpennigBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountOpeningNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountOpeningDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Files = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountOpennigBalances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Files = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningBalances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountOpeningBalanceEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit = table.Column<float>(type: "real", nullable: true),
                    Credit = table.Column<float>(type: "real", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountOpeningBalanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountOpeningBalanceEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountOpeningBalanceEntry_AccountOpennigBalances_AccountOpeningBalanceId",
                        column: x => x.AccountOpeningBalanceId,
                        principalTable: "AccountOpennigBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpeningBalanceEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BiltyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiltyDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Broker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit = table.Column<float>(type: "real", nullable: true),
                    Credit = table.Column<float>(type: "real", nullable: true),
                    OpeningBalanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningBalanceEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningBalanceEntry_OpeningBalances_OpeningBalanceId",
                        column: x => x.OpeningBalanceId,
                        principalTable: "OpeningBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8d22c778-ebae-42ff-bd96-4a9c766abfef", "AQAAAAIAAYagAAAAEOozyWt6rQUu196nHOvZznBw2EcfKr0ewpRrz67DsD21LWp92fUNCdFYp2Q/h8LT4w==" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountOpeningBalanceEntry_AccountOpeningBalanceId",
                table: "AccountOpeningBalanceEntry",
                column: "AccountOpeningBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningBalanceEntry_OpeningBalanceId",
                table: "OpeningBalanceEntry",
                column: "OpeningBalanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountOpeningBalanceEntry");

            migrationBuilder.DropTable(
                name: "OpeningBalanceEntry");

            migrationBuilder.DropTable(
                name: "AccountOpennigBalances");

            migrationBuilder.DropTable(
                name: "OpeningBalances");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80aae2ce-a6a1-4f20-964c-28454173a732", "AQAAAAIAAYagAAAAEGpjqE138WtZqoOe0X+MnUeSL+csaB4K3IK/fFsGPRVmidyPYDa6pNtMHYuB18MW5g==" });
        }
    }
}
