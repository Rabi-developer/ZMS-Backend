using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class hfsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "DeliveryBreakup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleDetail",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "Amounts",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Buyer",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Color",
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

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SampleDetail",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Gsm",
                table: "contracts",
                newName: "GSM");

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
                newName: "SelvegeWeave");

            migrationBuilder.RenameColumn(
                name: "TotalAmountMultiple",
                table: "contracts",
                newName: "SelvegeSubOptions");

            migrationBuilder.RenameColumn(
                name: "Shrinkage",
                table: "contracts",
                newName: "PickInsertionSubOptions");

            migrationBuilder.RenameColumn(
                name: "SelvegeWeaves",
                table: "contracts",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "Seller",
                table: "contracts",
                newName: "InductionThreadSubOptions");

            migrationBuilder.RenameColumn(
                name: "PickRate",
                table: "contracts",
                newName: "FinalSubOptions");

            migrationBuilder.RenameColumn(
                name: "LabDispNo",
                table: "contracts",
                newName: "EndUseSubOptions");

            migrationBuilder.RenameColumn(
                name: "LabDispDate",
                table: "contracts",
                newName: "DescriptionSubOptions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AdditionalInfo",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "SampleDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SampleReceivedDate",
                table: "SampleDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SampleDeliveredDate",
                table: "SampleDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "SampleDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractId",
                table: "SampleDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SampleDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "SampleDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SampleDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SampleDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "SampleDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "SampleDetail",
                type: "datetime2",
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdationDate",
                table: "contracts",
                type: "datetime2",
                nullable: true,
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Referdate",
                table: "contracts",
                type: "datetime2",
                nullable: true,
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractOwner",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractNumber",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "contracts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SampleDetailId",
                table: "AdditionalInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SampleDetail",
                table: "SampleDetail",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BuyerDeliveryBreakup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerDeliveryBreakup", x => x.id);
                    table.ForeignKey(
                        name: "FK_BuyerDeliveryBreakup_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversionContractRow",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionContractRow", x => x.id);
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
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietContractRow", x => x.id);
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
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiWidthContractRow", x => x.id);
                    table.ForeignKey(
                        name: "FK_MultiWidthContractRow_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerDeliveryBreakup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerDeliveryBreakup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerDeliveryBreakup_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommisionInfo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    BuyerCommission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConversionContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DietContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MultiWidthContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommisionInfo", x => x.id);
                    table.ForeignKey(
                        name: "FK_CommisionInfo_ConversionContractRow_ConversionContractRowid",
                        column: x => x.ConversionContractRowid,
                        principalTable: "ConversionContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommisionInfo_DietContractRow_DietContractRowid",
                        column: x => x.DietContractRowid,
                        principalTable: "DietContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommisionInfo_MultiWidthContractRow_MultiWidthContractRowid",
                        column: x => x.MultiWidthContractRowid,
                        principalTable: "MultiWidthContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a45f62a1-cbcd-422f-b8ed-d5f53b5a522b", "AQAAAAIAAYagAAAAEHSuHhl3qnth4JofdsqilVczssg1r3kF6cjCXnEOZsX9vrraxNhjCHZSMj7Hqwj0bA==" });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_BuyerId",
                table: "contracts",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_SellerId",
                table: "contracts",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerDeliveryBreakup_ContractId",
                table: "BuyerDeliveryBreakup",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CommisionInfo_ConversionContractRowid",
                table: "CommisionInfo",
                column: "ConversionContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_CommisionInfo_DietContractRowid",
                table: "CommisionInfo",
                column: "DietContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_CommisionInfo_MultiWidthContractRowid",
                table: "CommisionInfo",
                column: "MultiWidthContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionContractRow_ContractId",
                table: "ConversionContractRow",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DietContractRow_ContractId",
                table: "DietContractRow",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiWidthContractRow_ContractId",
                table: "MultiWidthContractRow",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerDeliveryBreakup_ContractId",
                table: "SellerDeliveryBreakup",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo",
                column: "SampleDetailId",
                principalTable: "SampleDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contracts_Buyers_BuyerId",
                table: "contracts",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contracts_Sellers_SellerId",
                table: "contracts",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_contracts_Buyers_BuyerId",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_contracts_Sellers_SellerId",
                table: "contracts");

            migrationBuilder.DropTable(
                name: "BuyerDeliveryBreakup");

            migrationBuilder.DropTable(
                name: "CommisionInfo");

            migrationBuilder.DropTable(
                name: "SellerDeliveryBreakup");

            migrationBuilder.DropTable(
                name: "ConversionContractRow");

            migrationBuilder.DropTable(
                name: "DietContractRow");

            migrationBuilder.DropTable(
                name: "MultiWidthContractRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleDetail",
                table: "SampleDetail");

            migrationBuilder.DropIndex(
                name: "IX_contracts_BuyerId",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_contracts_SellerId",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "contracts");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SampleDetail",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GSM",
                table: "contracts",
                newName: "Gsm");

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
                name: "SelvegeWeave",
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
                newName: "SelvegeWeaves");

            migrationBuilder.RenameColumn(
                name: "InductionThreadSubOptions",
                table: "contracts",
                newName: "Seller");

            migrationBuilder.RenameColumn(
                name: "FinalSubOptions",
                table: "contracts",
                newName: "PickRate");

            migrationBuilder.RenameColumn(
                name: "EndUseSubOptions",
                table: "contracts",
                newName: "LabDispNo");

            migrationBuilder.RenameColumn(
                name: "DescriptionSubOptions",
                table: "contracts",
                newName: "LabDispDate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AdditionalInfo",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UpdateDate",
                table: "SampleDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SampleReceivedDate",
                table: "SampleDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SampleDeliveredDate",
                table: "SampleDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreationDate",
                table: "SampleDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractId",
                table: "SampleDetail",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "WeftYarnType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdationDate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stuff",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Referdate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryDate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreationDate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractType",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContractOwner",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContractNumber",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "BranchId",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedDate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Amounts",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Buyer",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "SampleDetailId",
                table: "AdditionalInfo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SampleDetail",
                table: "SampleDetail",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DeliveryBreakup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "274c41eb-9e7f-4f0c-85ac-38157345a774", "AQAAAAIAAYagAAAAEJZt8sA86TjLB4O8OLsxlAlOV5M16gyHCdiuS3MlGDuX3jTFgz1Q1HbCryjfVPpfBQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ContractId",
                table: "DeliveryBreakup",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ContractId1",
                table: "DeliveryBreakup",
                column: "ContractId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo",
                column: "SampleDetailId",
                principalTable: "SampleDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
