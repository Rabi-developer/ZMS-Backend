using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLBillPaymentInvoiceupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VehicleNo",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "BiltyPaymentInvoiceLine",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "AmountCharges",
                table: "BiltyPaymentInvoiceLine",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdditionalLine",
                table: "BiltyPaymentInvoiceLine",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameCharges",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "87c37524-7f43-4ed9-a9f1-a2563966057a", "AQAAAAIAAYagAAAAEOsrJuC7/sAOu2JdZv4yQpNpUk5l1pcAopj47ZPiJuoIbjbJizwHD8Mc0nAPdyDUJw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountCharges",
                table: "BiltyPaymentInvoiceLine");

            migrationBuilder.DropColumn(
                name: "IsAdditionalLine",
                table: "BiltyPaymentInvoiceLine");

            migrationBuilder.DropColumn(
                name: "NameCharges",
                table: "BiltyPaymentInvoiceLine");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleNo",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "BiltyPaymentInvoiceLine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "BiltyPaymentInvoiceLine",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "267621e2-8822-4a0f-a8a6-95dd5aca1b57", "AQAAAAIAAYagAAAAEMKPfVbjilVzrROnmsEcBy1O1IOf073ccl1NOhquOIdpfhL3WgpM/XXCaipJghkOUQ==" });
        }
    }
}
