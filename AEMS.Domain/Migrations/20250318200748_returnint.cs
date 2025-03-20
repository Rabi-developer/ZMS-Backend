using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class returnint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CapitalAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentAccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentAccountId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapitalAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapitalAccounts_CapitalAccounts_ParentAccountId1",
                        column: x => x.ParentAccountId1,
                        principalTable: "CapitalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66ff6fd7-c6f3-478f-a9c4-2df007f495bc", "AQAAAAIAAYagAAAAEGBWaJ6v9U9W+Xj48hEJx/xeyoUd6rjxfTMUkmd/7ohtcpvdD4HmIv1tPwMBTaVPQw==" });

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_ParentAccountId1",
                table: "CapitalAccounts",
                column: "ParentAccountId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapitalAccounts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d2cb3bd-a938-45fe-bf7d-31f9f908998f", "AQAAAAIAAYagAAAAEBSkiNBGgKQ8UbrnzI74pP4HnHG8SWlwuqKRBnS3whmjAEBpi63PnqwR5wXRfqrtWw==" });
        }
    }
}
