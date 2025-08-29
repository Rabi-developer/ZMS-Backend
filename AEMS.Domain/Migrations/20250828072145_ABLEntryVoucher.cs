using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLEntryVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntryVoucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositSlipNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_EntryVoucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoucherDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit1 = table.Column<float>(type: "real", nullable: true),
                    Credit1 = table.Column<float>(type: "real", nullable: true),
                    CurrentBalance1 = table.Column<float>(type: "real", nullable: true),
                    ProjectedBalance1 = table.Column<float>(type: "real", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debit2 = table.Column<float>(type: "real", nullable: true),
                    Credit2 = table.Column<float>(type: "real", nullable: true),
                    CurrentBalance2 = table.Column<float>(type: "real", nullable: true),
                    ProjectedBalance2 = table.Column<float>(type: "real", nullable: true),
                    EntryVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_EntryVoucher_EntryVoucherId",
                        column: x => x.EntryVoucherId,
                        principalTable: "EntryVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "868c5f64-e927-4590-b333-733e807431b2", "AQAAAAIAAYagAAAAEIKN4wIY9GqAHYOJSOqfL0He21Yg7o/x/IUs6rG53sY/QQSVLXo98m6keW+2iIKdAA==" });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_EntryVoucherId",
                table: "VoucherDetail",
                column: "EntryVoucherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherDetail");

            migrationBuilder.DropTable(
                name: "EntryVoucher");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3437aa95-df12-4ee2-84e7-ddca54e4d7c3", "AQAAAAIAAYagAAAAEOuv8mUS7kNYyZAv5FYLQ4Y5PYdcf0OQbq03u8KMYy0IzFA+qRp4dmBytIRTIVrdCA==" });
        }
    }
}
