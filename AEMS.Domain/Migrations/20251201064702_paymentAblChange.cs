using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class paymentAblChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProgress");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentAmount",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaidAmount",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PDC",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Advanced",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2c38589-6cf2-4d26-b7ee-c531859795c3", "AQAAAAIAAYagAAAAEDIXjI+eCRQEQhMgLz9lSoE7+6Xr17vjS1TQ/rr5SwgfFs5Q3F0IlkF0a6vyBfFASA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PaymentAmount",
                table: "PaymentABL",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PaidAmount",
                table: "PaymentABL",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PDC",
                table: "PaymentABL",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Advanced",
                table: "PaymentABL",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingCompleted = table.Column<bool>(type: "bit", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChargesCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ChargesCount = table.Column<int>(type: "int", nullable: false),
                    ChargesPaidCount = table.Column<int>(type: "int", nullable: false),
                    ConsignmentCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ConsignmentCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentStep = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastConsignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaidChargesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidPaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentCompleted = table.Column<bool>(type: "bit", nullable: false),
                    PaymentCount = table.Column<int>(type: "int", nullable: false),
                    PaymentNos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentsCompletedCount = table.Column<int>(type: "int", nullable: false),
                    ProgressHints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressPercentage = table.Column<int>(type: "int", nullable: false),
                    ReceiptCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ReceiptCount = table.Column<int>(type: "int", nullable: false),
                    ReceiptNos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalChargesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalReceiptAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProgress_BookingOrder_BookingOrderId",
                        column: x => x.BookingOrderId,
                        principalTable: "BookingOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f218215-5132-43e8-b44e-ba51dfc6e313", "AQAAAAIAAYagAAAAEDOacZLbuv8UCwfngybp0s7aYgq+nA8tjnCqxUVdiDlUsBAHcQINTPxH1AHoZ8mRZA==" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProgress_BookingOrderId",
                table: "OrderProgress",
                column: "BookingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProgress_OrderNo",
                table: "OrderProgress",
                column: "OrderNo");
        }
    }
}
