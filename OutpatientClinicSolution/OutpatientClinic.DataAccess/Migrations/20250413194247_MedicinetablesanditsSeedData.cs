using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutpatientClinic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MedicinetablesanditsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Prescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DefaultDosage = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ForAdult = table.Column<bool>(type: "bit", nullable: false),
                    ForChildren = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicines__1234567890ABCDEF", x => x.MedicineId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ebe90ec-cb24-4db6-accc-c3f114e80a27", null, "Patient", "PATIENT" },
                    { "b34e7ba8-542e-422e-8d60-afc5b204ee3c", null, "Nurse", "NURSE" },
                    { "bdfea6fa-93d0-4ef6-93b9-c2c74ef8df26", null, "Admin", "ADMIN" },
                    { "c89b4dff-03b6-40b5-88cc-8039ef54e096", null, "Staff", "STAFF" },
                    { "ce266598-a64a-4e2d-84cd-a6c48729b237", null, "Doctor", "DOCTOR" },
                    { "f26fb5d7-07ab-4796-81a4-643350d3bc80", null, "Receptionist", "RECEPTIONIST" },
                    { "f4d1029a-bec2-4dc9-8f0c-b661c06d1284", null, "Technical_Support", "TECHNICAL_SUPPORT" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "MedicineId", "CreatedBy", "CreatedDate", "DefaultDosage", "Description", "ForAdult", "ForChildren", "IsDeleted", "Name", "Type", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7631), "500mg", "For mild to moderate pain and fever", true, true, false, "Paracetamol", "Tablet", null, null },
                    { 2, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7727), "400mg", "NSAID for pain/inflammation", true, true, false, "Ibuprofen", "Tablet", null, null },
                    { 3, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7737), "81mg", "Pain relief and antiplatelet", true, false, false, "Aspirin", "Tablet", null, null },
                    { 4, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7744), "220mg", "Long-lasting NSAID", true, false, false, "Naproxen", "Tablet", null, null },
                    { 5, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7752), "50mg", "Opioid for moderate-severe pain", true, false, false, "Tramadol", "Capsule", null, null },
                    { 6, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7760), "500mg", "Penicillin-type antibiotic", true, true, false, "Amoxicillin", "Capsule", null, null },
                    { 7, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7788), "250mg", "Macrolide antibiotic", true, true, false, "Azithromycin", "Tablet", null, null },
                    { 8, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7796), "500mg", "Fluoroquinolone antibiotic", true, false, false, "Ciprofloxacin", "Tablet", null, null },
                    { 9, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7876), "100mg", "Tetracycline antibiotic", true, false, false, "Doxycycline", "Capsule", null, null },
                    { 10, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7888), "400mg", "For anaerobic infections", true, true, false, "Metronidazole", "Tablet", null, null },
                    { 11, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7901), "10mg", "Non-drowsy allergy relief", true, true, false, "Loratadine", "Tablet", null, null },
                    { 12, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(7908), "10mg", "24-hour allergy relief", true, true, false, "Cetirizine", "Tablet", null, null },
                    { 13, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9112), "180mg", "Non-sedating antihistamine", true, false, false, "Fexofenadine", "Tablet", null, null },
                    { 14, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9264), "25mg", "For allergies and sleep aid", true, true, false, "Diphenhydramine", "Capsule", null, null },
                    { 15, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9275), "4mg/5ml", "Liquid antihistamine", true, true, false, "Chlorpheniramine", "Syrup", null, null },
                    { 16, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9336), "20mg", "Proton pump inhibitor", true, true, false, "Omeprazole", "Capsule", null, null },
                    { 17, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9344), "150mg", "H2 blocker for heartburn", true, true, false, "Ranitidine", "Tablet", null, null },
                    { 18, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9351), "500mg", "Fast-acting antacid", true, true, false, "Calcium Carbonate", "Chewable", null, null },
                    { 19, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9359), "400mg/5ml", "Liquid antacid", true, true, false, "Magnesium Hydroxide", "Liquid", null, null },
                    { 20, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9371), "20mg", "Acid reducer", true, true, false, "Famotidine", "Tablet", null, null },
                    { 21, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9379), "50mg", "SSRI antidepressant", true, false, false, "Sertraline", "Tablet", null, null },
                    { 22, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9386), "20mg", "SSRI for depression/OCD", true, true, false, "Fluoxetine", "Capsule", null, null },
                    { 23, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9394), "75mg", "SNRI antidepressant", true, false, false, "Venlafaxine", "Tablet", null, null },
                    { 24, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9404), "150mg", "Atypical antidepressant", true, false, false, "Bupropion", "Tablet", null, null },
                    { 25, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9410), "10mg", "SSRI for anxiety/depression", true, false, false, "Escitalopram", "Tablet", null, null },
                    { 26, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9417), "500mg", "Type 2 diabetes management", true, false, false, "Metformin", "Tablet", null, null },
                    { 27, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9424), "100 units/ml", "Long-acting insulin", true, true, false, "Insulin Glargine", "Injection", null, null },
                    { 28, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9431), "80mg", "Sulfonylurea for diabetes", true, false, false, "Gliclazide", "Tablet", null, null },
                    { 29, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9437), "10mg", "SGLT2 inhibitor", true, false, false, "Empagliflozin", "Tablet", null, null },
                    { 30, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9452), "1.2mg", "GLP-1 receptor agonist", true, false, false, "Liraglutide", "Injection", null, null },
                    { 31, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9514), "100mcg/dose", "Relief of asthma symptoms", true, true, false, "Salbutamol", "Inhaler", null, null },
                    { 32, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9525), "0.5mg/ml", "For COPD", true, true, false, "Ipratropium", "Nebulizer", null, null },
                    { 33, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9532), "12mcg/dose", "Long-acting bronchodilator", true, false, false, "Formoterol", "Inhaler", null, null },
                    { 34, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9538), "200mg", "For chronic asthma", true, false, false, "Theophylline", "Tablet", null, null },
                    { 35, null, new DateTime(2025, 4, 13, 21, 42, 42, 848, DateTimeKind.Local).AddTicks(9545), "5mg", "Leukotriene receptor antagonist", true, true, false, "Montelukast", "Chewable", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Name",
                table: "Medicines",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "MedicineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebe90ec-cb24-4db6-accc-c3f114e80a27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b34e7ba8-542e-422e-8d60-afc5b204ee3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdfea6fa-93d0-4ef6-93b9-c2c74ef8df26");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c89b4dff-03b6-40b5-88cc-8039ef54e096");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce266598-a64a-4e2d-84cd-a6c48729b237");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f26fb5d7-07ab-4796-81a4-643350d3bc80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4d1029a-bec2-4dc9-8f0c-b661c06d1284");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Prescriptions");

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
    }
}
