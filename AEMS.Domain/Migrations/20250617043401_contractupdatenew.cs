using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class contractupdatenew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "Amounts",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "BuyerCommission",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "BuyerRemark",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "CommissionFrom",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "CommissionPercentage",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "CommissionType",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "CommissionValue",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "DeliveryTerms",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "DispatchAddress",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "FabricRate",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Finish",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "LabDispDate",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "LabDispNo",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "PaymentTermsBuyer",
                table: "contracts");

            migrationBuilder.RenameColumn(
                name: "Wrapwt",
                table: "contracts",
                newName: "WeftYarnTypeSubOptions");

            migrationBuilder.RenameColumn(
                name: "WrapBag",
                table: "contracts",
                newName: "WeavesSubOptions");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "contracts",
                newName: "WarpYarnTypeSubOptions");

            migrationBuilder.RenameColumn(
                name: "Weftwt",
                table: "contracts",
                newName: "StuffSubOptions");

            migrationBuilder.RenameColumn(
                name: "WeftBag",
                table: "contracts",
                newName: "SelvegeWeaveSubOptions");

            migrationBuilder.RenameColumn(
                name: "TotalBag",
                table: "contracts",
                newName: "SelvegeThicknessSubOptions");

            migrationBuilder.RenameColumn(
                name: "TotalAmountMultiple",
                table: "contracts",
                newName: "SelvegeSubOptions");

            migrationBuilder.RenameColumn(
                name: "Shrinkage",
                table: "contracts",
                newName: "PickInsertionSubOptions");

            migrationBuilder.RenameColumn(
                name: "SellerRemark",
                table: "contracts",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "SellerCommission",
                table: "contracts",
                newName: "InductionThreadSubOptions");

            migrationBuilder.RenameColumn(
                name: "PickRate",
                table: "contracts",
                newName: "EndUseSubOptions");

            migrationBuilder.RenameColumn(
                name: "PaymentTermsSeller",
                table: "contracts",
                newName: "DescriptionSubOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "ConversionContractRowId",
                table: "DeliveryBreakup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConversionContractRowId1",
                table: "DeliveryBreakup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DietContractRowId",
                table: "DeliveryBreakup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DietContractRowId1",
                table: "DeliveryBreakup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MultiWidthContractRowId",
                table: "DeliveryBreakup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MultiWidthContractRowId1",
                table: "DeliveryBreakup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WeftYarnType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stuff",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FabricType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CommisionInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentTermsSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermsBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndUseSubOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispatchLater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommisionInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConversionContractRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amounts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wrapwt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weftwt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WrapBag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeftBag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmountMultiple = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommisionInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionContractRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionContractRow_CommisionInfo_CommisionInfoId",
                        column: x => x.CommisionInfoId,
                        principalTable: "CommisionInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConversionContractRow_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietContractRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabDispatchNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDispatchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Finish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountTotal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shrinkage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommisionInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietContractRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietContractRow_CommisionInfo_CommisionInfoId",
                        column: x => x.CommisionInfoId,
                        principalTable: "CommisionInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietContractRow_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultiWidthContractRow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FabricValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommisionInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiWidthContractRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiWidthContractRow_CommisionInfo_CommisionInfoId",
                        column: x => x.CommisionInfoId,
                        principalTable: "CommisionInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultiWidthContractRow_contracts_ContractId",
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
                values: new object[] { "04983d3e-f74c-43c9-9816-006b1fa0a772", "AQAAAAIAAYagAAAAEEMoKX0RnGq4ETYWJTvTzzKWF2u6gqKVNiGJdP6myTFB3JCqSX9kvJXQhozBxtJn2w==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowId",
                table: "DeliveryBreakup",
                column: "ConversionContractRowId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowId1",
                table: "DeliveryBreakup",
                column: "ConversionContractRowId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_DietContractRowId",
                table: "DeliveryBreakup",
                column: "DietContractRowId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_DietContractRowId1",
                table: "DeliveryBreakup",
                column: "DietContractRowId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowId",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowId1",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionContractRow_CommisionInfoId",
                table: "ConversionContractRow",
                column: "CommisionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionContractRow_ContractId",
                table: "ConversionContractRow",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DietContractRow_CommisionInfoId",
                table: "DietContractRow",
                column: "CommisionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DietContractRow_ContractId",
                table: "DietContractRow",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiWidthContractRow_CommisionInfoId",
                table: "MultiWidthContractRow",
                column: "CommisionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiWidthContractRow_ContractId",
                table: "MultiWidthContractRow",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowId",
                table: "DeliveryBreakup",
                column: "ConversionContractRowId",
                principalTable: "ConversionContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowId1",
                table: "DeliveryBreakup",
                column: "ConversionContractRowId1",
                principalTable: "ConversionContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowId",
                table: "DeliveryBreakup",
                column: "DietContractRowId",
                principalTable: "DietContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowId1",
                table: "DeliveryBreakup",
                column: "DietContractRowId1",
                principalTable: "DietContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowId",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowId",
                principalTable: "MultiWidthContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowId1",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowId1",
                principalTable: "MultiWidthContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropTable(
                name: "ConversionContractRow");

            migrationBuilder.DropTable(
                name: "DietContractRow");

            migrationBuilder.DropTable(
                name: "MultiWidthContractRow");

            migrationBuilder.DropTable(
                name: "CommisionInfo");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBreakup_DietContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBreakup_DietContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropColumn(
                name: "ConversionContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropColumn(
                name: "ConversionContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropColumn(
                name: "DietContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropColumn(
                name: "DietContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.DropColumn(
                name: "MultiWidthContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropColumn(
                name: "MultiWidthContractRowId1",
                table: "DeliveryBreakup");

            migrationBuilder.RenameColumn(
                name: "WeftYarnTypeSubOptions",
                table: "contracts",
                newName: "Wrapwt");

            migrationBuilder.RenameColumn(
                name: "WeavesSubOptions",
                table: "contracts",
                newName: "WrapBag");

            migrationBuilder.RenameColumn(
                name: "WarpYarnTypeSubOptions",
                table: "contracts",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "StuffSubOptions",
                table: "contracts",
                newName: "Weftwt");

            migrationBuilder.RenameColumn(
                name: "SelvegeWeaveSubOptions",
                table: "contracts",
                newName: "WeftBag");

            migrationBuilder.RenameColumn(
                name: "SelvegeThicknessSubOptions",
                table: "contracts",
                newName: "TotalBag");

            migrationBuilder.RenameColumn(
                name: "SelvegeSubOptions",
                table: "contracts",
                newName: "TotalAmountMultiple");

            migrationBuilder.RenameColumn(
                name: "PickInsertionSubOptions",
                table: "contracts",
                newName: "Shrinkage");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "contracts",
                newName: "SellerRemark");

            migrationBuilder.RenameColumn(
                name: "InductionThreadSubOptions",
                table: "contracts",
                newName: "SellerCommission");

            migrationBuilder.RenameColumn(
                name: "EndUseSubOptions",
                table: "contracts",
                newName: "PickRate");

            migrationBuilder.RenameColumn(
                name: "DescriptionSubOptions",
                table: "contracts",
                newName: "PaymentTermsSeller");

            migrationBuilder.AlterColumn<string>(
                name: "WeftYarnType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Stuff",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FabricType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "BuyerRemark",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionFrom",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionPercentage",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommissionValue",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTerms",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DispatchAddress",
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
                name: "PaymentTermsBuyer",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SampleDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDeliveredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleReceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Labs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YarnBags = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                values: new object[] { "274c41eb-9e7f-4f0c-85ac-38157345a774", "AQAAAAIAAYagAAAAEJZt8sA86TjLB4O8OLsxlAlOV5M16gyHCdiuS3MlGDuX3jTFgz1Q1HbCryjfVPpfBQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfo_SampleDetailId",
                table: "AdditionalInfo",
                column: "SampleDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleDetail_ContractId",
                table: "SampleDetail",
                column: "ContractId");
        }
    }
}
