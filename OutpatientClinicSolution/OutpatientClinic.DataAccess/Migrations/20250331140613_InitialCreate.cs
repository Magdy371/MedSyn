using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2be4b4e2-0c49-4c5c-803c-200043f826d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bf64e3e-e446-4669-8c41-47dae4259340");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e7e77e-c9c8-47a4-82f3-bc12d3fe54cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8db636e5-1382-4ad2-a7a7-573d56eca16a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ab0b1a7-af7e-4779-a6ca-2206dd7653f9", null, "Admin", "ADMIN" },
                    { "42776e20-0c43-4b77-b485-8a38a732d4b6", null, "Staff", "STAFF" },
                    { "47352fe0-89a0-4070-a70f-7efd21c81c31", null, "Patient", "PATIENT" },
                    { "e250037d-e5d1-4024-9758-06c607acf810", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab0b1a7-af7e-4779-a6ca-2206dd7653f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42776e20-0c43-4b77-b485-8a38a732d4b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47352fe0-89a0-4070-a70f-7efd21c81c31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e250037d-e5d1-4024-9758-06c607acf810");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2be4b4e2-0c49-4c5c-803c-200043f826d2", null, "Admin", "ADMIN" },
                    { "3bf64e3e-e446-4669-8c41-47dae4259340", null, "Patient", "PATIENT" },
                    { "60e7e77e-c9c8-47a4-82f3-bc12d3fe54cb", null, "Doctor", "DOCTOR" },
                    { "8db636e5-1382-4ad2-a7a7-573d56eca16a", null, "Staff", "STAFF" }
                });
        }
    }
}
