using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class deliverytermss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GstValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tolerance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Packing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceLength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchLater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shrinkage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDispNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDispDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryDetail_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca68621b-14f1-4b0b-bab3-3515d698e6f8", "AQAAAAIAAYagAAAAEMyclOE9tMJrkVpVWNI2N1+TdmS9NglWljF0UOopya8JkZwCuCfauJOp3vf0m6SyvQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetail_ContractId",
                table: "DeliveryDetail",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryDetail");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e747d38-682d-41c9-96ab-3ecc5d0abe2f", "AQAAAAIAAYagAAAAEMIGamkrGyCQ+neM69KuRI6vw5c1Bb/WxdsS9ahbAF8OYNW4QsOc/V5b13WwtXKd4A==" });
        }
    }
}
