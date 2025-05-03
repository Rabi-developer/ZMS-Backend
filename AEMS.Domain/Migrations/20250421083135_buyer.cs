using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class buyer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MTN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayableCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payableid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc3eb717-9cb4-4f76-9833-5313e50da5bc", "AQAAAAIAAYagAAAAEN1DkA6GcRg+uEeGMwtO441kSKbyqC5yrWfGFDzmn4AwqDXigRYoRm3JpzF4HWuGJg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e94d84b-5151-4506-807d-74d7c92833f2", "AQAAAAIAAYagAAAAEPl6ukIisaa7rUbp6AzBfSRWt0ikoe+4K22XopmN3QFhgvEvVCULgb/seZZorMFlYg==" });
        }
    }
}
