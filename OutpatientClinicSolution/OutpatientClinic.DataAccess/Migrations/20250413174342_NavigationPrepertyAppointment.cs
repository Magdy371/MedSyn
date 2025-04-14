using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NavigationPrepertyAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c430c49-d53f-48f8-addc-1e9362286ef7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d44079e-2e1b-4063-96de-0f09e5ce0402");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d8d9e7c-abff-4d7d-8078-5c1621dc98a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79b8285b-7b5f-426e-a3b7-68acd681de6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87a29c8d-c0d2-4eb2-9a9a-16c5967a8efd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88090703-357b-4642-80ee-f1544ebc53f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4ba43c0-e5d4-48b8-a1ec-ee8bbc4d31bc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50b430eb-7d06-4f23-aa36-6a49f7bd6dcd", null, "Doctor", "DOCTOR" },
                    { "9578a417-6151-458f-9ec9-c9490e56f7e3", null, "Admin", "ADMIN" },
                    { "cf6b8bad-2532-4746-a74f-82c5ee41f99f", null, "Receptionist", "RECEPTIONIST" },
                    { "dceb27e1-bef7-45be-9f5a-0001ec6570e3", null, "Staff", "STAFF" },
                    { "e269824f-ba2e-40f2-932a-58d42c1b9cea", null, "Nurse", "NURSE" },
                    { "eb07caf9-0291-4631-b981-13b9e273ea05", null, "Patient", "PATIENT" },
                    { "f1c12b63-bfa2-4455-9dab-7e1c8dc4806f", null, "Technical_Support", "TECHNICAL_SUPPORT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50b430eb-7d06-4f23-aa36-6a49f7bd6dcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9578a417-6151-458f-9ec9-c9490e56f7e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf6b8bad-2532-4746-a74f-82c5ee41f99f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceb27e1-bef7-45be-9f5a-0001ec6570e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e269824f-ba2e-40f2-932a-58d42c1b9cea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb07caf9-0291-4631-b981-13b9e273ea05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1c12b63-bfa2-4455-9dab-7e1c8dc4806f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c430c49-d53f-48f8-addc-1e9362286ef7", null, "Nurse", "NURSE" },
                    { "0d44079e-2e1b-4063-96de-0f09e5ce0402", null, "Staff", "STAFF" },
                    { "4d8d9e7c-abff-4d7d-8078-5c1621dc98a1", null, "Patient", "PATIENT" },
                    { "79b8285b-7b5f-426e-a3b7-68acd681de6a", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "87a29c8d-c0d2-4eb2-9a9a-16c5967a8efd", null, "Admin", "ADMIN" },
                    { "88090703-357b-4642-80ee-f1544ebc53f7", null, "Doctor", "DOCTOR" },
                    { "a4ba43c0-e5d4-48b8-a1ec-ee8bbc4d31bc", null, "Receptionist", "RECEPTIONIST" }
                });
        }
    }
}
