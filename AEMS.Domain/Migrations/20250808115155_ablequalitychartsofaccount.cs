using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ablequalitychartsofaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equality",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Listid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixedAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equality_Equality_ParentAccountId",
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
                values: new object[] { "d4ee6c40-cdc8-425a-a2cc-90b9fab70d6b", "AQAAAAIAAYagAAAAEP6FuyDVsSbQscRQ60XrRjiNj//2vMnrklmqVhWtB39NLntnZ/dKiQ1AhAI3Hf8azw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Equality_ParentAccountId",
                table: "Equality",
                column: "ParentAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equality");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c14664a-5190-4fcd-a35a-20d1dab7cd64", "AQAAAAIAAYagAAAAENG9LjAUxwpEyeCIK1dvqFVoFlVRkMdLUSdgChm3bVTpI3RMQxIAyaXdDqc9/jaVTw==" });
        }
    }
}
