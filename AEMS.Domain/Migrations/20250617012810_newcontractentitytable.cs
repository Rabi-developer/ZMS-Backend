using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newcontractentitytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CommisionInfo_DietContractRow_DietContractRowid",
                table: "CommisionInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CommisionInfo_MultiWidthContractRow_MultiWidthContractRowid",
                table: "CommisionInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_contracts_Buyers_BuyerId",
                table: "contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_contracts_Sellers_SellerId",
                table: "contracts");

            migrationBuilder.DropTable(
                name: "BuyerDeliveryBreakup");

            migrationBuilder.DropTable(
                name: "SellerDeliveryBreakup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleDetail",
                table: "SampleDetail");

            migrationBuilder.DropIndex(
                name: "IX_contracts_BuyerId",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_contracts_SellerId",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_CommisionInfo_DietContractRowid",
                table: "CommisionInfo");

            migrationBuilder.DropIndex(
                name: "IX_CommisionInfo_MultiWidthContractRowid",
                table: "CommisionInfo");

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

            migrationBuilder.DropColumn(
                name: "DietContractRowid",
                table: "CommisionInfo");

            migrationBuilder.DropColumn(
                name: "MultiWidthContractRowid",
                table: "CommisionInfo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SampleDetail",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GSM",
                table: "contracts",
                newName: "Gsm");

            migrationBuilder.RenameColumn(
                name: "SelvegeWeave",
                table: "contracts",
                newName: "SelvegeWeaves");

            migrationBuilder.RenameColumn(
                name: "FinalSubOptions",
                table: "contracts",
                newName: "SelvegeThicknessSubOptions");

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
                name: "UpdationDate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Referdate",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
                name: "Buyer",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishWidth",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Seller",
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
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConversionContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DietContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MultiWidthContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryBreakup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowid",
                        column: x => x.ConversionContractRowid,
                        principalTable: "ConversionContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryBreakup_DietContractRow_DietContractRowid",
                        column: x => x.DietContractRowid,
                        principalTable: "DietContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowid",
                        column: x => x.MultiWidthContractRowid,
                        principalTable: "MultiWidthContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "DietContractRowList",
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
                    DietContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietContractRowList", x => x.id);
                    table.ForeignKey(
                        name: "FK_DietContractRowList_DietContractRow_DietContractRowid",
                        column: x => x.DietContractRowid,
                        principalTable: "DietContractRow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultiWidthContractRowInfo",
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
                    MultiWidthContractRowid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiWidthContractRowInfo", x => x.id);
                    table.ForeignKey(
                        name: "FK_MultiWidthContractRowInfo_MultiWidthContractRow_MultiWidthContractRowid",
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
                values: new object[] { "0f9a5f52-ba25-47f5-87e3-9265bb0711f7", "AQAAAAIAAYagAAAAEI4KnhoOABN/00iPDSlgm6RhWg9oQE74tDNcwR5kQW0VGMB3zqk9aL67ikvoYgiEtg==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ContractId",
                table: "DeliveryBreakup",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ContractId1",
                table: "DeliveryBreakup",
                column: "ContractId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowid",
                table: "DeliveryBreakup",
                column: "ConversionContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_DietContractRowid",
                table: "DeliveryBreakup",
                column: "DietContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowid",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_DietContractRowList_DietContractRowid",
                table: "DietContractRowList",
                column: "DietContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_MultiWidthContractRowInfo_MultiWidthContractRowid",
                table: "MultiWidthContractRowInfo",
                column: "MultiWidthContractRowid");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo",
                column: "SampleDetailId",
                principalTable: "SampleDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfo_SampleDetail_SampleDetailId",
                table: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "DeliveryBreakup");

            migrationBuilder.DropTable(
                name: "DietContractRowList");

            migrationBuilder.DropTable(
                name: "MultiWidthContractRowInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleDetail",
                table: "SampleDetail");

            migrationBuilder.DropColumn(
                name: "Buyer",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "FinishWidth",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Seller",
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
                name: "SelvegeWeaves",
                table: "contracts",
                newName: "SelvegeWeave");

            migrationBuilder.RenameColumn(
                name: "SelvegeThicknessSubOptions",
                table: "contracts",
                newName: "FinalSubOptions");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdationDate",
                table: "contracts",
                type: "datetime2",
                nullable: true,
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

            migrationBuilder.AddColumn<Guid>(
                name: "DietContractRowid",
                table: "CommisionInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MultiWidthContractRowid",
                table: "CommisionInfo",
                type: "uniqueidentifier",
                nullable: true);

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
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "SellerDeliveryBreakup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Qty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f02c19f-a14d-41d1-a0fd-336e2902e2f4", "AQAAAAIAAYagAAAAEAzxjenO76krgO4T7Pjn9Q6spSJ0uKuu7uD0IpBRhK4A/wONRfw/Bgeb4SrzslbtIw==" });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_BuyerId",
                table: "contracts",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_SellerId",
                table: "contracts",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommisionInfo_DietContractRowid",
                table: "CommisionInfo",
                column: "DietContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_CommisionInfo_MultiWidthContractRowid",
                table: "CommisionInfo",
                column: "MultiWidthContractRowid");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerDeliveryBreakup_ContractId",
                table: "BuyerDeliveryBreakup",
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
                name: "FK_CommisionInfo_DietContractRow_DietContractRowid",
                table: "CommisionInfo",
                column: "DietContractRowid",
                principalTable: "DietContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommisionInfo_MultiWidthContractRow_MultiWidthContractRowid",
                table: "CommisionInfo",
                column: "MultiWidthContractRowid",
                principalTable: "MultiWidthContractRow",
                principalColumn: "id",
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
    }
}
