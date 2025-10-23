using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class rollwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "RelatedConsignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClaimType",
                value: "All");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClaimType",
                value: "Organization");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimType",
                value: "OrganizationUser");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClaimType",
                value: "Home");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClaimType",
                value: "Home");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClaimType",
                value: "Branch");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClaimType",
                value: "Section");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClaimType",
                value: "Department");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "ClaimType",
                value: "Home");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4b060397-f167-419e-a368-402bb48877c8"), null, true, false, "User", "USER" },
                    { new Guid("a1a1a1a1-0000-1111-2222-333344445555"), null, true, false, "Owner", "OWNER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "UserId" },
                values: new object[] { "925c46a6-8358-4f0e-8637-7c3ed09f24d9", "admin@ZMS.com", "ADMIN@ZMS.COM", "AQAAAAIAAYagAAAAEETkpXP4kVEm5JTosXfZI7lMpaV2SYicTqQSeBWZrwrk9YGCRTYh0jdPfjWVEBPpvA==", null });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("3ab833eb-917b-4d11-8d13-08dc96dae48d"),
                columns: new[] { "Description", "Email", "Name", "Website" },
                values: new object[] { "Commision Based Company", "zms@gmail.com", "ZMS", "http://www.ZMS.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Discriminator", "RoleId" },
                values: new object[,]
                {
                    { 121, "Hotel", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 131, "Organization", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 141, "Branch", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 151, "Department", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 155, "Room", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 159, "Booking", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 169, "Bank", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 179, "Agent", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 189, "AgentWork", "Read,Create,Update,Execute,Delete", "IdentityRoleClaim<Guid>", new Guid("a1a1a1a1-0000-1111-2222-333344445555") },
                    { 345, "Hotel", "Read", "IdentityRoleClaim<Guid>", new Guid("4b060397-f167-419e-a368-402bb48877c8") },
                    { 355, "Room", "Read", "IdentityRoleClaim<Guid>", new Guid("4b060397-f167-419e-a368-402bb48877c8") },
                    { 365, "Booking", "Read,Create", "IdentityRoleClaim<Guid>", new Guid("4b060397-f167-419e-a368-402bb48877c8") },
                    { 395, "Bank", "Create,Update", "IdentityRoleClaim<Guid>", new Guid("4b060397-f167-419e-a368-402bb48877c8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4b060397-f167-419e-a368-402bb48877c8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-0000-1111-2222-333344445555"));

            migrationBuilder.DropColumn(
                name: "Files",
                table: "RelatedConsignments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClaimType",
                value: "Resource_All");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClaimType",
                value: "Resource_Organization");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClaimType",
                value: "Resource_OrganizationUser");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClaimType",
                value: "Resource_Home");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClaimType",
                value: "Resource_Home");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClaimType",
                value: "Resource_Branch");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClaimType",
                value: "Resource_Section");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClaimType",
                value: "Resource_Department");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "ClaimType",
                value: "Resource_Home");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc9544a9-4e5c-4032-a27f-3001b29364c5"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "5a5871dc-e5b7-48b7-8970-3243b37a492d", "admin@AEMS.com", "ADMIN@AEMS.COM", "AQAAAAIAAYagAAAAEGoQw//kciBCzYq5uOX+6gMmeNkUzH62hj0RcPK9+oiVLal65OpMgmoL2fNIENJpgA==" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: new Guid("3ab833eb-917b-4d11-8d13-08dc96dae48d"),
                columns: new[] { "Description", "Email", "Name", "Website" },
                values: new object[] { "An Education Management System", "admin@AEMS.com", "AEMS", "http://www.AEMS.com" });
        }
    }
}
