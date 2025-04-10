using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Appointment_issueFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3343f39a-3faf-4b42-ad80-e978229d57e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "380c2017-1989-45f1-a87d-00d85ca6a182");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fde6ca3-bb78-44d1-9aaf-5f11acb6d923");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56dce52e-61b1-471a-a289-ac957bbaa8e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "652851bf-b069-4a81-b939-3b629cd1a5eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c20f7cea-5409-4225-8adb-7d086da01aaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f63bc467-2c60-4c4f-9d28-9898a7179eb2");

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

            migrationBuilder.AddForeignKey(
                name: "FK__Appointme__Depar__4BC73F29",
                table: "Appointments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Appointme__Depar__4BC73F29",
                table: "Appointments");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3343f39a-3faf-4b42-ad80-e978229d57e1", null, "Patient", "PATIENT" },
                    { "380c2017-1989-45f1-a87d-00d85ca6a182", null, "Admin", "ADMIN" },
                    { "3fde6ca3-bb78-44d1-9aaf-5f11acb6d923", null, "Receptionist", "RECEPTIONIST" },
                    { "56dce52e-61b1-471a-a289-ac957bbaa8e2", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "652851bf-b069-4a81-b939-3b629cd1a5eb", null, "Nurse", "NURSE" },
                    { "c20f7cea-5409-4225-8adb-7d086da01aaf", null, "Doctor", "DOCTOR" },
                    { "f63bc467-2c60-4c4f-9d28-9898a7179eb2", null, "Staff", "STAFF" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
