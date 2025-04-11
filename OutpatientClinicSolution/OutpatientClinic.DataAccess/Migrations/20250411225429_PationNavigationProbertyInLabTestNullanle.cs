using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PationNavigationProbertyInLabTestNullanle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d99e4dc-681d-4afc-8f05-debc44df9905");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41917002-6444-4a5a-b1d3-d5051343ccd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e616808-6f4e-4abd-8ff9-e973cc1d28a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "606dbfbb-bd1c-4d1e-8efd-13639cbd0595");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "820cc16f-b1be-4cfc-a5c5-be5a19593b73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9139b282-4dbd-4923-bb9b-bb3fa814e5f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2c16abf-3ad3-45e8-bf31-8cc99b3f8261");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1faed417-1ef1-42e3-931b-300bf7220fee", null, "Receptionist", "RECEPTIONIST" },
                    { "63a99620-2480-4ded-b5a8-05885d03c892", null, "Patient", "PATIENT" },
                    { "670d2989-6b5a-4418-94de-72ba11c2b3c2", null, "Admin", "ADMIN" },
                    { "6a74bc51-b0e2-4ca5-a8de-7b96e9a3da48", null, "Staff", "STAFF" },
                    { "876c211e-4ab8-4c2b-8fb5-b3ce4dbbdd20", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "bbb7ca62-fb2b-4bcd-bb94-c02c3e2a27c7", null, "Doctor", "DOCTOR" },
                    { "d9a44cf3-ccb9-46d6-997b-7e2fdde547dc", null, "Nurse", "NURSE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1faed417-1ef1-42e3-931b-300bf7220fee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63a99620-2480-4ded-b5a8-05885d03c892");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "670d2989-6b5a-4418-94de-72ba11c2b3c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a74bc51-b0e2-4ca5-a8de-7b96e9a3da48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "876c211e-4ab8-4c2b-8fb5-b3ce4dbbdd20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbb7ca62-fb2b-4bcd-bb94-c02c3e2a27c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9a44cf3-ccb9-46d6-997b-7e2fdde547dc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d99e4dc-681d-4afc-8f05-debc44df9905", null, "Staff", "STAFF" },
                    { "41917002-6444-4a5a-b1d3-d5051343ccd8", null, "Receptionist", "RECEPTIONIST" },
                    { "5e616808-6f4e-4abd-8ff9-e973cc1d28a7", null, "Admin", "ADMIN" },
                    { "606dbfbb-bd1c-4d1e-8efd-13639cbd0595", null, "Patient", "PATIENT" },
                    { "820cc16f-b1be-4cfc-a5c5-be5a19593b73", null, "Nurse", "NURSE" },
                    { "9139b282-4dbd-4923-bb9b-bb3fa814e5f1", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "c2c16abf-3ad3-45e8-bf31-8cc99b3f8261", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
