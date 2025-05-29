using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class invoiceee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhtValue",
                table: "RelatedInvoiceContract",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "TotalInvoiceValue",
                table: "RelatedInvoiceContract",
                newName: "Wht");

            migrationBuilder.RenameColumn(
                name: "InvoiceValueWithGst",
                table: "RelatedInvoiceContract",
                newName: "WeftYarnType");

            migrationBuilder.RenameColumn(
                name: "GstValue",
                table: "RelatedInvoiceContract",
                newName: "WeftCount");

            migrationBuilder.RenameColumn(
                name: "FabricDetails",
                table: "RelatedInvoiceContract",
                newName: "Weaves");

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                table: "RelatedInvoiceContract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Final",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOfEnds",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOfPicks",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Selvedge",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarpCount",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarpYarnType",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "04167b77-ab92-440a-b021-15137fa600b2", "AQAAAAIAAYagAAAAEFpGpqgk5chpzbEbPbcmce+DAGY9Sj1CUlFaJSJGWscXQuGo4Xc1r5jH22XhMaIU6A==" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedInvoiceContract_ContractId",
                table: "RelatedInvoiceContract",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedInvoiceContract_contracts_ContractId",
                table: "RelatedInvoiceContract",
                column: "ContractId",
                principalTable: "contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedInvoiceContract_contracts_ContractId",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropIndex(
                name: "IX_RelatedInvoiceContract_ContractId",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "Final",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "NoOfEnds",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "NoOfPicks",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "Selvedge",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "WarpCount",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "WarpYarnType",
                table: "RelatedInvoiceContract");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "RelatedInvoiceContract",
                newName: "WhtValue");

            migrationBuilder.RenameColumn(
                name: "Wht",
                table: "RelatedInvoiceContract",
                newName: "TotalInvoiceValue");

            migrationBuilder.RenameColumn(
                name: "WeftYarnType",
                table: "RelatedInvoiceContract",
                newName: "InvoiceValueWithGst");

            migrationBuilder.RenameColumn(
                name: "WeftCount",
                table: "RelatedInvoiceContract",
                newName: "GstValue");

            migrationBuilder.RenameColumn(
                name: "Weaves",
                table: "RelatedInvoiceContract",
                newName: "FabricDetails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0196c6c6-e648-42c9-b367-7e9c1349815a", "AQAAAAIAAYagAAAAEN3auN+QRrK5e/fRRO2KNs5/K5GPOXsVKit2jyZcuowXP9mMYIW95D5vuFic99R8gA==" });
        }
    }
}
