using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07fa03b7-dfa8-4922-8c59-6ca84d0fcc4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59ad1d61-6db7-49d6-98f6-9e53c44ae022");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "928b52b7-4884-4365-92f2-cbecdd3e5938");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b81762-1cd7-4779-8a24-7bbbee3acc85");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patients");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07fa03b7-dfa8-4922-8c59-6ca84d0fcc4f", null, "Admin", "ADMIN" },
                    { "59ad1d61-6db7-49d6-98f6-9e53c44ae022", null, "Patient", "PATIENT" },
                    { "928b52b7-4884-4365-92f2-cbecdd3e5938", null, "Staff", "STAFF" },
                    { "d1b81762-1cd7-4779-8a24-7bbbee3acc85", null, "Doctor", "DOCTOR" }
                });
        }
    }
}
