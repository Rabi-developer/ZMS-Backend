using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class parentidincorrecrnameinallchartsofaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AblAssests_AblAssests_AblAssestsId",
                table: "AblAssests");

            migrationBuilder.DropForeignKey(
                name: "FK_AblAssests_Equality_ParentAccountId",
                table: "AblAssests");

            migrationBuilder.DropForeignKey(
                name: "FK_AblExpense_AblExpense_AblExpenseId",
                table: "AblExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_AblExpense_Equality_ParentAccountId",
                table: "AblExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_AblLiabilities_AblLiabilities_AblLiabilitiesId",
                table: "AblLiabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_AblLiabilities_Equality_ParentAccountId",
                table: "AblLiabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_AblRevenue_AblRevenue_AblRevenueId",
                table: "AblRevenue");

            migrationBuilder.DropForeignKey(
                name: "FK_AblRevenue_Equality_ParentAccountId",
                table: "AblRevenue");

            migrationBuilder.DropIndex(
                name: "IX_AblRevenue_AblRevenueId",
                table: "AblRevenue");

            migrationBuilder.DropIndex(
                name: "IX_AblLiabilities_AblLiabilitiesId",
                table: "AblLiabilities");

            migrationBuilder.DropIndex(
                name: "IX_AblExpense_AblExpenseId",
                table: "AblExpense");

            migrationBuilder.DropIndex(
                name: "IX_AblAssests_AblAssestsId",
                table: "AblAssests");

            migrationBuilder.DropColumn(
                name: "AblRevenueId",
                table: "AblRevenue");

            migrationBuilder.DropColumn(
                name: "AblLiabilitiesId",
                table: "AblLiabilities");

            migrationBuilder.DropColumn(
                name: "AblExpenseId",
                table: "AblExpense");

            migrationBuilder.DropColumn(
                name: "AblAssestsId",
                table: "AblAssests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eec8baa8-cf45-4e22-bb88-67e0b6bad132", "AQAAAAIAAYagAAAAELPWiio7VDTnGxdhF/cbYlFPG7+ZB3WiS8LGg6nHK/u12jeX8jhQYmrPJz8/463sXQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_AblAssests_AblAssests_ParentAccountId",
                table: "AblAssests",
                column: "ParentAccountId",
                principalTable: "AblAssests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblExpense_AblExpense_ParentAccountId",
                table: "AblExpense",
                column: "ParentAccountId",
                principalTable: "AblExpense",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblLiabilities_AblLiabilities_ParentAccountId",
                table: "AblLiabilities",
                column: "ParentAccountId",
                principalTable: "AblLiabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblRevenue_AblRevenue_ParentAccountId",
                table: "AblRevenue",
                column: "ParentAccountId",
                principalTable: "AblRevenue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AblAssests_AblAssests_ParentAccountId",
                table: "AblAssests");

            migrationBuilder.DropForeignKey(
                name: "FK_AblExpense_AblExpense_ParentAccountId",
                table: "AblExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_AblLiabilities_AblLiabilities_ParentAccountId",
                table: "AblLiabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_AblRevenue_AblRevenue_ParentAccountId",
                table: "AblRevenue");

            migrationBuilder.AddColumn<Guid>(
                name: "AblRevenueId",
                table: "AblRevenue",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AblLiabilitiesId",
                table: "AblLiabilities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AblExpenseId",
                table: "AblExpense",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AblAssestsId",
                table: "AblAssests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2f667c11-8b01-46be-b7c8-17dce4139539", "AQAAAAIAAYagAAAAEO3p2HY1gOaBa2FVIF98BKMhVRWalxhOXtCJM1hsxGGVQ5NdznpiIGF7RVKjM9P28Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_AblRevenue_AblRevenueId",
                table: "AblRevenue",
                column: "AblRevenueId");

            migrationBuilder.CreateIndex(
                name: "IX_AblLiabilities_AblLiabilitiesId",
                table: "AblLiabilities",
                column: "AblLiabilitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_AblExpense_AblExpenseId",
                table: "AblExpense",
                column: "AblExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_AblAssests_AblAssestsId",
                table: "AblAssests",
                column: "AblAssestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AblAssests_AblAssests_AblAssestsId",
                table: "AblAssests",
                column: "AblAssestsId",
                principalTable: "AblAssests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblAssests_Equality_ParentAccountId",
                table: "AblAssests",
                column: "ParentAccountId",
                principalTable: "Equality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblExpense_AblExpense_AblExpenseId",
                table: "AblExpense",
                column: "AblExpenseId",
                principalTable: "AblExpense",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblExpense_Equality_ParentAccountId",
                table: "AblExpense",
                column: "ParentAccountId",
                principalTable: "Equality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblLiabilities_AblLiabilities_AblLiabilitiesId",
                table: "AblLiabilities",
                column: "AblLiabilitiesId",
                principalTable: "AblLiabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblLiabilities_Equality_ParentAccountId",
                table: "AblLiabilities",
                column: "ParentAccountId",
                principalTable: "Equality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblRevenue_AblRevenue_AblRevenueId",
                table: "AblRevenue",
                column: "AblRevenueId",
                principalTable: "AblRevenue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AblRevenue_Equality_ParentAccountId",
                table: "AblRevenue",
                column: "ParentAccountId",
                principalTable: "Equality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
