using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class intinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wht",
                table: "RelatedInvoiceContract",
                newName: "InvoiceValueWithGst");

            migrationBuilder.AddColumn<string>(
                name: "FabricDetails",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gst",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GstValue",
                table: "RelatedInvoiceContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0196c6c6-e648-42c9-b367-7e9c1349815a", "AQAAAAIAAYagAAAAEN3auN+QRrK5e/fRRO2KNs5/K5GPOXsVKit2jyZcuowXP9mMYIW95D5vuFic99R8gA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FabricDetails",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "Gst",
                table: "RelatedInvoiceContract");

            migrationBuilder.DropColumn(
                name: "GstValue",
                table: "RelatedInvoiceContract");

            migrationBuilder.RenameColumn(
                name: "InvoiceValueWithGst",
                table: "RelatedInvoiceContract",
                newName: "Wht");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b931c294-55da-4dd5-af62-656aec38cb36", "AQAAAAIAAYagAAAAEJz6xKpQSiTtp1KrOySLpreKsHSLkPQSF5IOyflMEiNN50skWCWhAVtWSt6rhIsj/Q==" });
        }
    }
}
