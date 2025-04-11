using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LabTestDateIbnlyTimeIssueFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13c11d9a-f738-4fe3-b136-3a65eb95fbc7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24273852-9694-49b8-9bce-18789b0ef133");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a697a3d-6cb1-42ac-90a6-0e8399f59b53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70b87573-fffb-4bfc-96e6-dccf840fa9ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98629520-286c-4e76-9c17-7b202a8c02cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0aecb10-005e-4d04-804c-03e1dd398ccf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd6c5ca1-1f62-4da6-a135-9d825cd21909");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TestDate",
                table: "LabTests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37ce9781-05a1-42d3-9aac-9dfc9c0ca74a", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "4a2cb263-5fd2-4d75-af6b-e6629cc30611", null, "Admin", "ADMIN" },
                    { "5d0d1aa1-d6a2-459d-a64b-0c5727d4fb2a", null, "Doctor", "DOCTOR" },
                    { "8a27e13f-1dc1-4b85-a456-c2b4f5fecc57", null, "Patient", "PATIENT" },
                    { "98b088f9-d9b7-48bf-aa24-c838f50957fe", null, "Receptionist", "RECEPTIONIST" },
                    { "eac40624-74cc-4f3a-86f0-de4b5de378ed", null, "Nurse", "NURSE" },
                    { "f8ee8eb9-02e1-459f-b29b-d6f3f6b20ac7", null, "Staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37ce9781-05a1-42d3-9aac-9dfc9c0ca74a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a2cb263-5fd2-4d75-af6b-e6629cc30611");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d0d1aa1-d6a2-459d-a64b-0c5727d4fb2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a27e13f-1dc1-4b85-a456-c2b4f5fecc57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98b088f9-d9b7-48bf-aa24-c838f50957fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eac40624-74cc-4f3a-86f0-de4b5de378ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8ee8eb9-02e1-459f-b29b-d6f3f6b20ac7");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "TestDate",
                table: "LabTests",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13c11d9a-f738-4fe3-b136-3a65eb95fbc7", null, "Patient", "PATIENT" },
                    { "24273852-9694-49b8-9bce-18789b0ef133", null, "Nurse", "NURSE" },
                    { "5a697a3d-6cb1-42ac-90a6-0e8399f59b53", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "70b87573-fffb-4bfc-96e6-dccf840fa9ab", null, "Receptionist", "RECEPTIONIST" },
                    { "98629520-286c-4e76-9c17-7b202a8c02cc", null, "Doctor", "DOCTOR" },
                    { "a0aecb10-005e-4d04-804c-03e1dd398ccf", null, "Admin", "ADMIN" },
                    { "fd6c5ca1-1f62-4da6-a135-9d825cd21909", null, "Staff", "STAFF" }
                });
        }
    }
}
