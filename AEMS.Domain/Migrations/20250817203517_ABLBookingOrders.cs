using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ABLBookingOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transporter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContainerNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Munshayana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookedDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetentionDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Via1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Via2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedReachedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReachedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleMunshyana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatedConsignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BiltyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecvAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DelDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedConsignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedConsignment_BookingOrder_BookingOrderId",
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
                values: new object[] { "99c04d0e-2877-4762-87bb-108677b594f1", "AQAAAAIAAYagAAAAEM3E22K/uFo/FU4TwTqbVlecLaMO8PobzLILCbIFgMBtFLCYTQDK+fVWGsiy3z2YZg==" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedConsignment_BookingOrderId",
                table: "RelatedConsignment",
                column: "BookingOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedConsignment");

            migrationBuilder.DropTable(
                name: "BookingOrder");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa65a8f0-4bf4-40ac-bb7c-c66fc6bcaae6", "AQAAAAIAAYagAAAAEBy9Cjr9OC8QIQ4lhGVr02vge1MGFzWRIujoQYoxowZcYhq5D+j/Xk0xOuf/sAtK7g==" });
        }
    }
}
