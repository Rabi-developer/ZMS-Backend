using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newonesmigrationitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryDetail");

            migrationBuilder.AddColumn<string>(
                name: "Amounts",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerCommission",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DispatchLater",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FabricRate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Finish",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabDispDate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabDispNo",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickRate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerCommission",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shrinkage",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalAmountMultiple",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalBag",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeftBag",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weftwt",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WrapBag",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wrapwt",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "274c41eb-9e7f-4f0c-85ac-38157345a774", "AQAAAAIAAYagAAAAEJZt8sA86TjLB4O8OLsxlAlOV5M16gyHCdiuS3MlGDuX3jTFgz1Q1HbCryjfVPpfBQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amounts",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "BuyerCommission",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "DispatchLater",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "FabricRate",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Finish",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "LabDispDate",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "LabDispNo",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "PickRate",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "SellerCommission",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Shrinkage",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "TotalAmountMultiple",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "TotalBag",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "WeftBag",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Weftwt",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "WrapBag",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Wrapwt",
                table: "contracts");

            migrationBuilder.CreateTable(
                name: "DeliveryDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchLater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GstValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDispDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDispNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Packing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceLength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shrinkage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tolerance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
