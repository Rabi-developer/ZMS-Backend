using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Domain.Migrations

{
    /// <inheritdoc />
    public partial class sadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeManagement",
                table: "EmployeeManagement");

            migrationBuilder.DropColumn(
                name: "PaidSalary",
                table: "EmployeeManagement");

            migrationBuilder.DropColumn(
                name: "RemainingSalary",
                table: "EmployeeManagement");

            migrationBuilder.RenameTable(
                name: "EmployeeManagement",
                newName: "EmployeeManagements");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeFirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "WorkLocation",
                table: "EmployeeManagements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Promotion",
                table: "EmployeeManagements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "EmployeeManagements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "EmployeeManagements",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "EmployeeManagements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeManagements",
                table: "EmployeeManagements",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d2cb3bd-a938-45fe-bf7d-31f9f908998f", "AQAAAAIAAYagAAAAEBSkiNBGgKQ8UbrnzI74pP4HnHG8SWlwuqKRBnS3whmjAEBpi63PnqwR5wXRfqrtWw==" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeManagements_EmployeeId",
                table: "EmployeeManagements",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeManagements_Employees_EmployeeId",
                table: "EmployeeManagements",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeManagements_Employees_EmployeeId",
                table: "EmployeeManagements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeManagements",
                table: "EmployeeManagements");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeManagements_EmployeeId",
                table: "EmployeeManagements");

            migrationBuilder.RenameTable(
                name: "EmployeeManagements",
                newName: "EmployeeManagement");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeFirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkLocation",
                table: "EmployeeManagement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Promotion",
                table: "EmployeeManagement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "EmployeeManagement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "EmployeeManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "EmployeeManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidSalary",
                table: "EmployeeManagement",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingSalary",
                table: "EmployeeManagement",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeManagement",
                table: "EmployeeManagement",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79d01bdf-acd7-4e76-9a73-716268eec0f1", "AQAAAAIAAYagAAAAEKmiH42Of38FasSf5TnBxXFt1KNK3+qxcJ/Yz2XgAH8MbTZigc/CGRQBMqaZyWatnQ==" });
        }
    }
}
