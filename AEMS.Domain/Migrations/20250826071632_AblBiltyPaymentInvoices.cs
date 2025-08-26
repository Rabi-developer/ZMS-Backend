using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AblBiltyPaymentInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiltyPaymentInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_BiltyPaymentInvoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiltyPaymentInvoiceLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Munshayana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Broker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiltyPaymentInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiltyPaymentInvoiceLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiltyPaymentInvoiceLine_BiltyPaymentInvoice_BiltyPaymentInvoiceId",
                        column: x => x.BiltyPaymentInvoiceId,
                        principalTable: "BiltyPaymentInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3437aa95-df12-4ee2-84e7-ddca54e4d7c3", "AQAAAAIAAYagAAAAEOuv8mUS7kNYyZAv5FYLQ4Y5PYdcf0OQbq03u8KMYy0IzFA+qRp4dmBytIRTIVrdCA==" });

            migrationBuilder.CreateIndex(
                name: "IX_BiltyPaymentInvoiceLine_BiltyPaymentInvoiceId",
                table: "BiltyPaymentInvoiceLine",
                column: "BiltyPaymentInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiltyPaymentInvoiceLine");

            migrationBuilder.DropTable(
                name: "BiltyPaymentInvoice");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "406cd302-3776-4897-bba3-55ea6717dd5d", "AQAAAAIAAYagAAAAEKBuZ/8gYgN3MVNYdoUgMo2bXTcwk+WcbyF/TqUrshiOQq19QAM6Q9DgOlIfhT07Xw==" });
        }
    }
}
