using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLRecipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Party = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesTaxOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesTaxRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhtOnSbr = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BiltyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiltyDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiltyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SrbAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReceiptAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc8b8046-0bc3-4214-8a23-1fa27aee08d7", "AQAAAAIAAYagAAAAEDtO+y1y6uer+EiYmtxwu4rUWWFXZfiQC0Y73g2I7ToDY0RlIMy3DZgs3vJJzN0lcQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ReceiptId",
                table: "ReceiptItem",
                column: "ReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptItem");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "083a6b60-b5d6-4fcb-8905-2826f211d616", "AQAAAAIAAYagAAAAEKKi5SuA8QQ6KmLeZMrbyEbWq+gqLYCRnSqBmkT58dIBk9XUb0ZAVUezI9Aim/EyLg==" });
        }
    }
}
