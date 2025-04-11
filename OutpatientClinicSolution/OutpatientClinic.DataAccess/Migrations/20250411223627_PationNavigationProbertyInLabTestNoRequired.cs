using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PationNavigationProbertyInLabTestNoRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "37ce9781-05a1-42d3-9aac-9dfc9c0ca74a", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "4a2cb263-5fd2-4d75-af6b-e6629cc30611", null, "Admin", "ADMIN" },
                    { "5d0d1aa1-d6a2-459d-a64b-0c5727d4fb2a", null, "Doctor", "DOCTOR" },
                    { "8a27e13f-1dc1-4b85-a456-c2b4f5fecc57", null, "Patient", "PATIENT" },
                    { "98b088f9-d9b7-48bf-aa24-c838f50957fe", null, "Receptionist", "RECEPTIONIST" },
                    { "eac40624-74cc-4f3a-86f0-de4b5de378ed", null, "Nurse", "NURSE" },
                    { "f8ee8eb9-02e1-459f-b29b-d6f3f6b20ac7", null, "Staff", "STAFF" }
                });
        }
    }
}
