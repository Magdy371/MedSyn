using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NavigationPropertyMedicalErrosNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "0c430c49-d53f-48f8-addc-1e9362286ef7", null, "Nurse", "NURSE" },
                    { "0d44079e-2e1b-4063-96de-0f09e5ce0402", null, "Staff", "STAFF" },
                    { "4d8d9e7c-abff-4d7d-8078-5c1621dc98a1", null, "Patient", "PATIENT" },
                    { "79b8285b-7b5f-426e-a3b7-68acd681de6a", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "87a29c8d-c0d2-4eb2-9a9a-16c5967a8efd", null, "Admin", "ADMIN" },
                    { "88090703-357b-4642-80ee-f1544ebc53f7", null, "Doctor", "DOCTOR" },
                    { "a4ba43c0-e5d4-48b8-a1ec-ee8bbc4d31bc", null, "Receptionist", "RECEPTIONIST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "1faed417-1ef1-42e3-931b-300bf7220fee", null, "Receptionist", "RECEPTIONIST" },
                    { "63a99620-2480-4ded-b5a8-05885d03c892", null, "Patient", "PATIENT" },
                    { "670d2989-6b5a-4418-94de-72ba11c2b3c2", null, "Admin", "ADMIN" },
                    { "6a74bc51-b0e2-4ca5-a8de-7b96e9a3da48", null, "Staff", "STAFF" },
                    { "876c211e-4ab8-4c2b-8fb5-b3ce4dbbdd20", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "bbb7ca62-fb2b-4bcd-bb94-c02c3e2a27c7", null, "Doctor", "DOCTOR" },
                    { "d9a44cf3-ccb9-46d6-997b-7e2fdde547dc", null, "Nurse", "NURSE" }
                });
        }
    }
}
