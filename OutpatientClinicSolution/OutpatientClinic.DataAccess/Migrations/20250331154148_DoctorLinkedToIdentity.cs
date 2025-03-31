using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DoctorLinkedToIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Staff");

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
    }
}
