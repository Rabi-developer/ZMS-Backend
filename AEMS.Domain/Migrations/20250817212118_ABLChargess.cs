using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLChargess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Charge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiltyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargeLine_Charges_ChargesId",
                        column: x => x.ChargesId,
                        principalTable: "Charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChargesPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaidAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankCash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChqNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChqDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargesPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargesPayments_Charges_ChargesId",
                        column: x => x.ChargesId,
                        principalTable: "Charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "083a6b60-b5d6-4fcb-8905-2826f211d616", "AQAAAAIAAYagAAAAEKKi5SuA8QQ6KmLeZMrbyEbWq+gqLYCRnSqBmkT58dIBk9XUb0ZAVUezI9Aim/EyLg==" });

            migrationBuilder.CreateIndex(
                name: "IX_ChargeLine_ChargesId",
                table: "ChargeLine",
                column: "ChargesId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargesPayments_ChargesId",
                table: "ChargesPayments",
                column: "ChargesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeLine");

            migrationBuilder.DropTable(
                name: "ChargesPayments");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e1cf3d7b-dae2-4399-9355-b40af2f28107", "AQAAAAIAAYagAAAAEBYomMckHSDUNPYkhwttDH8Owx0aPAUj6XXcO4h6pW8ggSEfX9/65n1SpazBBDkACg==" });
        }
    }
}
