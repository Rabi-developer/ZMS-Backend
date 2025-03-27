using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newreturnint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssetsId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LiabilitiesId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RevenueId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_CapitalAccounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "CapitalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_CapitalAccounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "CapitalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Liabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Liabilities_CapitalAccounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "CapitalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revenues_CapitalAccounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "CapitalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8549828c-3311-434c-bb15-e8a61ffeaa4f", "AQAAAAIAAYagAAAAEA1RtkiuQovVr6gGAT8J4jXExqS2dgPtbL8sWgBvo9+2TwRGN0usNkVE7GDouSSjdw==" });

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_AssetsId",
                table: "CapitalAccounts",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_ExpenseId",
                table: "CapitalAccounts",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_LiabilitiesId",
                table: "CapitalAccounts",
                column: "LiabilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_RevenueId",
                table: "CapitalAccounts",
                column: "RevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ParentAccountId",
                table: "Assets",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ParentAccountId",
                table: "Expenses",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Liabilities_ParentAccountId",
                table: "Liabilities",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_ParentAccountId",
                table: "Revenues",
                column: "ParentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Assets_AssetsId",
                table: "CapitalAccounts",
                column: "AssetsId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Expenses_ExpenseId",
                table: "CapitalAccounts",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Liabilities_LiabilitiesId",
                table: "CapitalAccounts",
                column: "LiabilitiesId",
                principalTable: "Liabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Revenues_RevenueId",
                table: "CapitalAccounts",
                column: "RevenueId",
                principalTable: "Revenues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Assets_AssetsId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Expenses_ExpenseId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Liabilities_LiabilitiesId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Revenues_RevenueId",
                table: "CapitalAccounts");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Liabilities");

            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_AssetsId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_ExpenseId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_LiabilitiesId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_RevenueId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "AssetsId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "LiabilitiesId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "RevenueId",
                table: "CapitalAccounts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ddfc92d-2cc1-4679-b447-b05e2b155e2a", "AQAAAAIAAYagAAAAENGtXLFZ+113WJA++G4KkyCKfKhug/03Ke6uT9KTHMnaEMfHKuy+GAOAP+Y9Rd2P7A==" });
        }
    }
}
