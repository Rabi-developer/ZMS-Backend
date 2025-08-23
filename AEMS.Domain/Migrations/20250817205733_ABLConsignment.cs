using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLConsignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsignmentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiltyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignmentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignmentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContainerNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Freight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SbrTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SprAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeliveryCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsuranceCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TollTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReceivedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IncomeTaxDed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IncomeTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Consignment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsignmentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QtyUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsignmentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsignmentItem_Consignment_ConsignmentId",
                        column: x => x.ConsignmentId,
                        principalTable: "Consignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e1cf3d7b-dae2-4399-9355-b40af2f28107", "AQAAAAIAAYagAAAAEBYomMckHSDUNPYkhwttDH8Owx0aPAUj6XXcO4h6pW8ggSEfX9/65n1SpazBBDkACg==" });

            migrationBuilder.CreateIndex(
                name: "IX_ConsignmentItem_ConsignmentId",
                table: "ConsignmentItem",
                column: "ConsignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsignmentItem");

            migrationBuilder.DropTable(
                name: "Consignment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99c04d0e-2877-4762-87bb-108677b594f1", "AQAAAAIAAYagAAAAEM3E22K/uFo/FU4TwTqbVlecLaMO8PobzLILCbIFgMBtFLCYTQDK+fVWGsiy3z2YZg==" });
        }
    }
}
