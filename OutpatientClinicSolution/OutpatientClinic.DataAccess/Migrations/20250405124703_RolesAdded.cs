using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70655e4b-4e05-4471-8b0e-10d629eef7a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fd3f02e-f8a8-4b98-8b8c-e07b2cc575ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97d770cc-521d-4ce3-93ac-8099bc7cd2e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3c6b1c1-c16f-4726-ab5c-065a6dd2c444");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33714576-54bd-41db-98e0-6b09c825722d", null, "Receptionist", "RECEPTIONIST" },
                    { "4076aafe-af31-4dda-bc21-f27eed0b63f7", null, "Staff", "STAFF" },
                    { "418d6e47-56c8-4048-a4e4-43afcd298e42", null, "Doctor", "DOCTOR" },
                    { "4295f948-27e4-4357-ab18-82ffacb6e436", null, "Admin", "ADMIN" },
                    { "57f65884-3c84-493e-ae8b-2ad0a8224243", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "c1891131-9264-47b7-93e2-b381846b4dbe", null, "Nurse", "NURSE" },
                    { "d49e60d7-1e2f-411f-9e96-3dec7e745b20", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33714576-54bd-41db-98e0-6b09c825722d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4076aafe-af31-4dda-bc21-f27eed0b63f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "418d6e47-56c8-4048-a4e4-43afcd298e42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4295f948-27e4-4357-ab18-82ffacb6e436");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57f65884-3c84-493e-ae8b-2ad0a8224243");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1891131-9264-47b7-93e2-b381846b4dbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d49e60d7-1e2f-411f-9e96-3dec7e745b20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70655e4b-4e05-4471-8b0e-10d629eef7a0", null, "Admin", "ADMIN" },
                    { "8fd3f02e-f8a8-4b98-8b8c-e07b2cc575ab", null, "Staff", "STAFF" },
                    { "97d770cc-521d-4ce3-93ac-8099bc7cd2e5", null, "Doctor", "DOCTOR" },
                    { "e3c6b1c1-c16f-4726-ab5c-065a6dd2c444", null, "Patient", "PATIENT" }
                });
        }
    }
}
