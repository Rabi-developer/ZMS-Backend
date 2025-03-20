using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class @return : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_CapitalAccounts_ParentAccountId1",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_ParentAccountId1",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "ParentAccountId1",
                table: "CapitalAccounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentAccountId",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CapitalAccounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Listid",
                table: "CapitalAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ddfc92d-2cc1-4679-b447-b05e2b155e2a", "AQAAAAIAAYagAAAAENGtXLFZ+113WJA++G4KkyCKfKhug/03Ke6uT9KTHMnaEMfHKuy+GAOAP+Y9Rd2P7A==" });

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_ParentAccountId",
                table: "CapitalAccounts",
                column: "ParentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_CapitalAccounts_ParentAccountId",
                table: "CapitalAccounts",
                column: "ParentAccountId",
                principalTable: "CapitalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapitalAccounts_CapitalAccounts_ParentAccountId",
                table: "CapitalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CapitalAccounts_ParentAccountId",
                table: "CapitalAccounts");

            migrationBuilder.DropColumn(
                name: "Listid",
                table: "CapitalAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "ParentAccountId",
                table: "CapitalAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CapitalAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentAccountId1",
                table: "CapitalAccounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66ff6fd7-c6f3-478f-a9c4-2df007f495bc", "AQAAAAIAAYagAAAAEGBWaJ6v9U9W+Xj48hEJx/xeyoUd6rjxfTMUkmd/7ohtcpvdD4HmIv1tPwMBTaVPQw==" });

            migrationBuilder.CreateIndex(
                name: "IX_CapitalAccounts_ParentAccountId1",
                table: "CapitalAccounts",
                column: "ParentAccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CapitalAccounts_CapitalAccounts_ParentAccountId1",
                table: "CapitalAccounts",
                column: "ParentAccountId1",
                principalTable: "CapitalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
