using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ablchartsofaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AblAssests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AblAssestsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AblAssests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AblAssests_AblAssests_AblAssestsId",
                        column: x => x.AblAssestsId,
                        principalTable: "AblAssests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AblAssests_Equality_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Equality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AblExpense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AblExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AblExpense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AblExpense_AblExpense_AblExpenseId",
                        column: x => x.AblExpenseId,
                        principalTable: "AblExpense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AblExpense_Equality_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Equality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AblLiabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AblLiabilitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AblLiabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AblLiabilities_AblLiabilities_AblLiabilitiesId",
                        column: x => x.AblLiabilitiesId,
                        principalTable: "AblLiabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AblLiabilities_Equality_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Equality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AblRevenue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AblRevenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AblRevenue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AblRevenue_AblRevenue_AblRevenueId",
                        column: x => x.AblRevenueId,
                        principalTable: "AblRevenue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AblRevenue_Equality_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Equality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "58a8de7c-98b2-4373-9417-973555268a50", "AQAAAAIAAYagAAAAEGHhYAPHtQ+XvA7WRcEFHkcQxwY43cYkT/8BEZa0iRo+d31eBNX99E+BhE6lcATCXQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AblAssests_AblAssestsId",
                table: "AblAssests",
                column: "AblAssestsId");

            migrationBuilder.CreateIndex(
                name: "IX_AblAssests_ParentAccountId",
                table: "AblAssests",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AblExpense_AblExpenseId",
                table: "AblExpense",
                column: "AblExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_AblExpense_ParentAccountId",
                table: "AblExpense",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AblLiabilities_AblLiabilitiesId",
                table: "AblLiabilities",
                column: "AblLiabilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_AblLiabilities_ParentAccountId",
                table: "AblLiabilities",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AblRevenue_AblRevenueId",
                table: "AblRevenue",
                column: "AblRevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_AblRevenue_ParentAccountId",
                table: "AblRevenue",
                column: "ParentAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AblAssests");

            migrationBuilder.DropTable(
                name: "AblExpense");

            migrationBuilder.DropTable(
                name: "AblLiabilities");

            migrationBuilder.DropTable(
                name: "AblRevenue");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4ee6c40-cdc8-425a-a2cc-90b9fab70d6b", "AQAAAAIAAYagAAAAEP6FuyDVsSbQscRQ60XrRjiNj//2vMnrklmqVhWtB39NLntnZ/dKiQ1AhAI3Hf8azw==" });
        }
    }
}
