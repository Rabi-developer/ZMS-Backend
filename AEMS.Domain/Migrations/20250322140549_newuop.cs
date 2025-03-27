using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newuop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_CapitalAccounts_ParentAccountId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Assets_AssetsId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Expenses_ExpenseId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Liabilities_LiabilitiesId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_Revenues_RevenueId",
                table: "CapitalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CapitalAccounts_ParentAccountId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Liabilities_CapitalAccounts_ParentAccountId",
                table: "Liabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_CapitalAccounts_ParentAccountId",
                table: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_AssetsId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_ExpenseId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_LiabilitiesId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_RevenueId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "AssetsId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "LiabilitiesId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "RevenueId",
                table: "CapitalAccounts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8b3681e-4440-43a4-bb8f-e5cfa862f5ef", "AQAAAAIAAYagAAAAEKE6pvKm1+FRbr1dFEW6A368hkjXlWtogPICmC14yFEyjkVgJ9i6b7d4rL4HfVPxgQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Assets_ParentAccountId",
                table: "Assets",
                column: "ParentAccountId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Expenses_ParentAccountId",
                table: "Expenses",
                column: "ParentAccountId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Liabilities_Liabilities_ParentAccountId",
                table: "Liabilities",
                column: "ParentAccountId",
                principalTable: "Liabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Revenues_ParentAccountId",
                table: "Revenues",
                column: "ParentAccountId",
                principalTable: "Revenues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Assets_ParentAccountId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Expenses_ParentAccountId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Liabilities_Liabilities_ParentAccountId",
                table: "Liabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Revenues_ParentAccountId",
                table: "Revenues");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetsId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LiabilitiesId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RevenueId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9174efa-b575-426c-b682-163b8732782d", "AQAAAAIAAYagAAAAEK83hJXSPM6GqjZQe54FaUUdYAYmPJ5AJjYNlfMs/LtrFRlZ+onda23+VXsOootEew==" });

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_AssetsId",
                table: "CapitalAccounts",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_ExpenseId",
                table: "CapitalAccounts",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_LiabilitiesId",
                table: "CapitalAccounts",
                column: "LiabilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_RevenueId",
                table: "CapitalAccounts",
                column: "RevenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_CapitalAccounts_ParentAccountId",
                table: "Assets",
                column: "ParentAccountId",
                principalTable: "CapitalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Assets_AssetsId",
                table: "CapitalAccounts",
                column: "AssetsId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Expenses_ExpenseId",
                table: "CapitalAccounts",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Liabilities_LiabilitiesId",
                table: "CapitalAccounts",
                column: "LiabilitiesId",
                principalTable: "Liabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_Revenues_RevenueId",
                table: "CapitalAccounts",
                column: "RevenueId",
                principalTable: "Revenues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CapitalAccounts_ParentAccountId",
                table: "Expenses",
                column: "ParentAccountId",
                principalTable: "CapitalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Liabilities_CapitalAccounts_ParentAccountId",
                table: "Liabilities",
                column: "ParentAccountId",
                principalTable: "CapitalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_CapitalAccounts_ParentAccountId",
                table: "Revenues",
                column: "ParentAccountId",
                principalTable: "CapitalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
