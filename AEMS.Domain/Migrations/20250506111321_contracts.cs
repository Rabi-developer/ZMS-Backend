using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class contracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stuff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlendRatio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlendType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarpCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarpYarnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeftCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeftYarnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfEnds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfPicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickInsertion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Final = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Selvedge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelvedgeWeave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelvedgeWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tolerance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Packing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceLength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GstValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryBreakup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryBreakup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryBreakup_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryBreakup_contracts_ContractId1",
                        column: x => x.ContractId1,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SampleDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleReceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDeliveredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleDetail_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YarnBags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Labs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                        column: x => x.SampleDetailId,
                        principalTable: "SampleDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fc36ccf-599c-4a3f-8b72-dde9da03e344", "AQAAAAIAAYagAAAAEKAipu0PzME9DCSISUMlSkg4YvmnNBNh9HcAeC4FcOdJK45LpkT+67GpIBrUUF6iKQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfo_SampleDetailId",
                table: "AdditionalInfo",
                column: "SampleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ContractId",
                table: "DeliveryBreakup",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ContractId1",
                table: "DeliveryBreakup",
                column: "ContractId1");

            migrationBuilder.CreateIndex(
                name: "IX_SampleDetail_ContractId",
                table: "SampleDetail",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "DeliveryBreakup");

            migrationBuilder.DropTable(
                name: "SampleDetail");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fc66ba6-98c9-4bc4-a831-0c041cb93826", "AQAAAAIAAYagAAAAEFUsTv819fsxpNsewTUQrULfou53YynBPApmt5R2X9vlMbL3EK3W5nqhIgmi0/xe+g==" });
        }
    }
}
