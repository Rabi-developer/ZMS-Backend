using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class j : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommisionInfo_ConversionContractRow_ConversionContractRowid",
                table: "CommisionInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowid",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowid",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowid",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DietContractRowList_DietContractRow_DietContractRowid",
                table: "DietContractRowList");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiWidthContractRowInfo_MultiWidthContractRow_MultiWidthContractRowid",
                table: "MultiWidthContractRowInfo");

            migrationBuilder.RenameColumn(
                name: "MultiWidthContractRowid",
                table: "MultiWidthContractRowInfo",
                newName: "MultiWidthContractRowId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MultiWidthContractRowInfo",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_MultiWidthContractRowInfo_MultiWidthContractRowid",
                table: "MultiWidthContractRowInfo",
                newName: "IX_MultiWidthContractRowInfo_MultiWidthContractRowId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MultiWidthContractRow",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DietContractRowid",
                table: "DietContractRowList",
                newName: "DietContractRowId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DietContractRowList",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DietContractRowList_DietContractRowid",
                table: "DietContractRowList",
                newName: "IX_DietContractRowList_DietContractRowId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DietContractRow",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MultiWidthContractRowid",
                table: "DeliveryBreakup",
                newName: "MultiWidthContractRowId");

            migrationBuilder.RenameColumn(
                name: "DietContractRowid",
                table: "DeliveryBreakup",
                newName: "DietContractRowId");

            migrationBuilder.RenameColumn(
                name: "ConversionContractRowid",
                table: "DeliveryBreakup",
                newName: "ConversionContractRowId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowid",
                table: "DeliveryBreakup",
                newName: "IX_DeliveryBreakup_MultiWidthContractRowId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBreakup_DietContractRowid",
                table: "DeliveryBreakup",
                newName: "IX_DeliveryBreakup_DietContractRowId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowid",
                table: "DeliveryBreakup",
                newName: "IX_DeliveryBreakup_ConversionContractRowId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ConversionContractRow",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ConversionContractRowid",
                table: "CommisionInfo",
                newName: "ConversionContractRowId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CommisionInfo",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CommisionInfo_ConversionContractRowid",
                table: "CommisionInfo",
                newName: "IX_CommisionInfo_ConversionContractRowId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0098a8f3-1197-49f5-b3dd-d42dca62c2e7", "AQAAAAIAAYagAAAAEGmpmaxufPN1k82pmQEyCdavSNjlJ8kMfcw0cVefDI4QlCPAfoIePJmbMDHGfx750w==" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommisionInfo_ConversionContractRow_ConversionContractRowId",
                table: "CommisionInfo",
                column: "ConversionContractRowId",
                principalTable: "ConversionContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowId",
                table: "DeliveryBreakup",
                column: "ConversionContractRowId",
                principalTable: "ConversionContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowId",
                table: "DeliveryBreakup",
                column: "DietContractRowId",
                principalTable: "DietContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowId",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowId",
                principalTable: "MultiWidthContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DietContractRowList_DietContractRow_DietContractRowId",
                table: "DietContractRowList",
                column: "DietContractRowId",
                principalTable: "DietContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MultiWidthContractRowInfo_MultiWidthContractRow_MultiWidthContractRowId",
                table: "MultiWidthContractRowInfo",
                column: "MultiWidthContractRowId",
                principalTable: "MultiWidthContractRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommisionInfo_ConversionContractRow_ConversionContractRowId",
                table: "CommisionInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowId",
                table: "DeliveryBreakup");

            migrationBuilder.DropForeignKey(
                name: "FK_DietContractRowList_DietContractRow_DietContractRowId",
                table: "DietContractRowList");

            migrationBuilder.DropForeignKey(
                name: "FK_MultiWidthContractRowInfo_MultiWidthContractRow_MultiWidthContractRowId",
                table: "MultiWidthContractRowInfo");

            migrationBuilder.RenameColumn(
                name: "MultiWidthContractRowId",
                table: "MultiWidthContractRowInfo",
                newName: "MultiWidthContractRowid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MultiWidthContractRowInfo",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_MultiWidthContractRowInfo_MultiWidthContractRowId",
                table: "MultiWidthContractRowInfo",
                newName: "IX_MultiWidthContractRowInfo_MultiWidthContractRowid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MultiWidthContractRow",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DietContractRowId",
                table: "DietContractRowList",
                newName: "DietContractRowid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DietContractRowList",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_DietContractRowList_DietContractRowId",
                table: "DietContractRowList",
                newName: "IX_DietContractRowList_DietContractRowid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DietContractRow",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MultiWidthContractRowId",
                table: "DeliveryBreakup",
                newName: "MultiWidthContractRowid");

            migrationBuilder.RenameColumn(
                name: "DietContractRowId",
                table: "DeliveryBreakup",
                newName: "DietContractRowid");

            migrationBuilder.RenameColumn(
                name: "ConversionContractRowId",
                table: "DeliveryBreakup",
                newName: "ConversionContractRowid");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBreakup_MultiWidthContractRowId",
                table: "DeliveryBreakup",
                newName: "IX_DeliveryBreakup_MultiWidthContractRowid");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBreakup_DietContractRowId",
                table: "DeliveryBreakup",
                newName: "IX_DeliveryBreakup_DietContractRowid");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBreakup_ConversionContractRowId",
                table: "DeliveryBreakup",
                newName: "IX_DeliveryBreakup_ConversionContractRowid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ConversionContractRow",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ConversionContractRowId",
                table: "CommisionInfo",
                newName: "ConversionContractRowid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CommisionInfo",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_CommisionInfo_ConversionContractRowId",
                table: "CommisionInfo",
                newName: "IX_CommisionInfo_ConversionContractRowid");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0b4a62a3-7d1a-4e93-b022-5ffc94b2d534", "AQAAAAIAAYagAAAAEEvsJoObtWS/fXvrXi+wr/HjyvsI4729dK0dA7/C2cI1otrhQb74fowz5ToRQmmOzA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommisionInfo_ConversionContractRow_ConversionContractRowid",
                table: "CommisionInfo",
                column: "ConversionContractRowid",
                principalTable: "ConversionContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_ConversionContractRow_ConversionContractRowid",
                table: "DeliveryBreakup",
                column: "ConversionContractRowid",
                principalTable: "ConversionContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_DietContractRow_DietContractRowid",
                table: "DeliveryBreakup",
                column: "DietContractRowid",
                principalTable: "DietContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBreakup_MultiWidthContractRow_MultiWidthContractRowid",
                table: "DeliveryBreakup",
                column: "MultiWidthContractRowid",
                principalTable: "MultiWidthContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DietContractRowList_DietContractRow_DietContractRowid",
                table: "DietContractRowList",
                column: "DietContractRowid",
                principalTable: "DietContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MultiWidthContractRowInfo_MultiWidthContractRow_MultiWidthContractRowid",
                table: "MultiWidthContractRowInfo",
                column: "MultiWidthContractRowid",
                principalTable: "MultiWidthContractRow",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
