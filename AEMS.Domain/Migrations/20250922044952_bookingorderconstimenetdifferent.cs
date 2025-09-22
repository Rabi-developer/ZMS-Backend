using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class bookingorderconstimenetdifferent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedConsignment_BookingOrder_BookingOrderId",
                table: "RelatedConsignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedConsignment",
                table: "RelatedConsignment");

            migrationBuilder.RenameTable(
                name: "RelatedConsignment",
                newName: "RelatedConsignments");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedConsignment_BookingOrderId",
                table: "RelatedConsignments",
                newName: "IX_RelatedConsignments_BookingOrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "RelatedConsignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "RelatedConsignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RelatedConsignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RelatedConsignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "RelatedConsignments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "RelatedConsignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedConsignments",
                table: "RelatedConsignments",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6e0a48a2-e928-41f8-902c-eb7961e599a9", "AQAAAAIAAYagAAAAEBcGzbYchpwokW7OQz3Eif4mBnLju1RpkOq2DEB1Au/evLPo9OH3WH2NfAdUopqppg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedConsignments_BookingOrder_BookingOrderId",
                table: "RelatedConsignments",
                column: "BookingOrderId",
                principalTable: "BookingOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedConsignments_BookingOrder_BookingOrderId",
                table: "RelatedConsignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedConsignments",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "RelatedConsignments");

            migrationBuilder.RenameTable(
                name: "RelatedConsignments",
                newName: "RelatedConsignment");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedConsignments_BookingOrderId",
                table: "RelatedConsignment",
                newName: "IX_RelatedConsignment_BookingOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedConsignment",
                table: "RelatedConsignment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ced2c3d1-f697-4794-bf41-557f8bcb61b0", "AQAAAAIAAYagAAAAECBJktVfE20/fltwGRoxVrYzX9c0XlpXzSdYUQBgvjUa8IceWqV+lDcLimGDgvwN/g==" });

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedConsignment_BookingOrder_BookingOrderId",
                table: "RelatedConsignment",
                column: "BookingOrderId",
                principalTable: "BookingOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
