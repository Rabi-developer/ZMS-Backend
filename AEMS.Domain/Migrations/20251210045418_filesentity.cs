using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class filesentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "WrapYarnTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "WeftYarnTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Weaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Vendor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "UnitofMeasures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Transporters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "TransporterCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Stuffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "StockReorders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "StockMovements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "SelvegeWidths",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "SelvegeWeaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "SelvegeThicknesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Selveges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "SalesTax",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Revenues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "RelatedConsignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Receipt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "ProjectTargets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Prices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "PickInsertion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Peicelengths",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "PaymentTerms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "PaymentABL",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Party",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Packings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Munshyana",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Liabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Levels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "InspectionNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "InspectionContract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "InductionThreads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Gsms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "GeneralSaleTexts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Finals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "FabricTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Equality",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "EntryVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "EndUses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "EmployeeManagements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "DispatchNotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Descriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "DeliveryTerms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Consignment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "CommisionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Charges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "CapitalAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Buyers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "BusinessAssociate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Brooker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "BlendRatio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "BiltyPaymentInvoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "AblRevenue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "AblLiabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "AblExpense",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "AblAssests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "Files", "PasswordHash" },
                values: new object[] { "c97ebbd3-567c-44e3-8a63-691f97f51198", null, "AQAAAAIAAYagAAAAELiuFJUiHNArUq2RvHVXYzvlO1ZZlGkiJL8qgIEvJHrDKPMz6EVXE3f7F9EgzKnJsw==" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("3ab833eb-917b-4d11-8d13-08dc96dae48d"),
                column: "Files",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Files",
                table: "WrapYarnTypes");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "WeftYarnTypes");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Weaves");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "UnitofMeasures");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Transporters");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "TransporterCompanies");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Stuffs");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "StockReorders");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "SelvegeWidths");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "SelvegeWeaves");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "SelvegeThicknesses");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Selveges");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "SalesTax");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Revenues");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "ProjectTargets");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "PickInsertion");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Peicelengths");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "PaymentABL");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Packings");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Munshyana");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Liabilities");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "InspectionNotes");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "InspectionContract");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "InductionThreads");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Gsms");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "GeneralSaleTexts");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Finals");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "FabricTypes");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Equality");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "EntryVoucher");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "EndUses");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "EmployeeManagements");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "DispatchNotes");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Descriptions");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "DeliveryTerms");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Consignment");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "CommisionTypes");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "BusinessAssociate");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Brooker");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "BlendRatio");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "BiltyPaymentInvoice");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "AblRevenue");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "AblLiabilities");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "AblExpense");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "AblAssests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2cc982c9-13d2-48a5-85a9-127fa2afd240", "AQAAAAIAAYagAAAAEAeYPJfd9Gh8Y9J1Q56Yuumk67NNQXncIUBsJ8iIMXs9UdWOp6BsR/XOzkHWseG2yQ==" });
        }
    }
}
