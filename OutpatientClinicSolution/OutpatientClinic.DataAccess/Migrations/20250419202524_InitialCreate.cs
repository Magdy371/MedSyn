using System;
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
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    ClinicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clinics__3347C2FDCF7CE5D9", x => x.ClinicID);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContactI__5C6625BBA84CC8E1", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__B2079BCD3678F47F", x => x.DepartmentID);
                });

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

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__4BE66694F74E0005", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    FacilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Facility__5FB08B94472946F9", x => x.FacilityID);
                    table.ForeignKey(
                        name: "FK__Facility__Contac__5DCAEF64",
                        column: x => x.ContactID,
                        principalTable: "ContactInfo",
                        principalColumn: "ContactID");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__96D4AAF719817DB0", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK__Staff__ContactID__36B12243",
                        column: x => x.ContactID,
                        principalTable: "ContactInfo",
                        principalColumn: "ContactID");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FacilityID = table.Column<int>(type: "int", nullable: true),
                    LastRestocked = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__727E83EB1BE9BB16", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK__Inventory__Facil__6383C8BA",
                        column: x => x.FacilityID,
                        principalTable: "Facility",
                        principalColumn: "FacilityID");
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrder",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierID = table.Column<int>(type: "int", nullable: true),
                    FacilityID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpectedDeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__C3905BAF90D0822A", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__SupplierO__Facil__6EF57B66",
                        column: x => x.FacilityID,
                        principalTable: "Facility",
                        principalColumn: "FacilityID");
                    table.ForeignKey(
                        name: "FK__SupplierO__Suppl__6E01572D",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID");
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctors__2DC00EDFCF437B0A", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK__Doctors__Departm__3E52440B",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK__Doctors__DoctorI__3D5E1FD2",
                        column: x => x.DoctorID,
                        principalTable: "Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNote",
                columns: table => new
                {
                    DeliveryNoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    FacilityID = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReceivedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__2A1CDD7E87E9E1FA", x => x.DeliveryNoteID);
                    table.ForeignKey(
                        name: "FK__DeliveryN__Facil__7D439ABD",
                        column: x => x.FacilityID,
                        principalTable: "Facility",
                        principalColumn: "FacilityID");
                    table.ForeignKey(
                        name: "FK__DeliveryN__Order__7C4F7684",
                        column: x => x.OrderID,
                        principalTable: "SupplierOrder",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ItemID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__D3B9D30C62611F41", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK__SupplierO__ItemI__76969D2E",
                        column: x => x.ItemID,
                        principalTable: "Inventory",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK__SupplierO__Order__75A278F5",
                        column: x => x.OrderID,
                        principalTable: "SupplierOrder",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: true),
                    PrimaryDoctorID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patients__970EC34675504678", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK__Patients__Contac__4222D4EF",
                        column: x => x.ContactID,
                        principalTable: "ContactInfo",
                        principalColumn: "ContactID");
                    table.ForeignKey(
                        name: "FK__Patients__Primar__4316F928",
                        column: x => x.PrimaryDoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNoteDetails",
                columns: table => new
                {
                    DeliveryDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryNoteID = table.Column<int>(type: "int", nullable: true),
                    ItemID = table.Column<int>(type: "int", nullable: true),
                    QuantityDelivered = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__EFD2C287E16C026E", x => x.DeliveryDetailID);
                    table.ForeignKey(
                        name: "FK__DeliveryN__Deliv__02FC7413",
                        column: x => x.DeliveryNoteID,
                        principalTable: "DeliveryNote",
                        principalColumn: "DeliveryNoteID");
                    table.ForeignKey(
                        name: "FK__DeliveryN__ItemI__03F0984C",
                        column: x => x.ItemID,
                        principalTable: "Inventory",
                        principalColumn: "ItemID");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ClinicID = table.Column<int>(type: "int", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Appointm__8ECDFCA2E6C3C030", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK__Appointme__Clini__4AB81AF0",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID");
                    table.ForeignKey(
                        name: "FK__Appointme__Depar__4BC73F29",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK__Appointme__Docto__49C3F6B7",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID");
                    table.ForeignKey(
                        name: "FK__Appointme__Patie__48CFD27E",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    InsuranceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__74231BC423DEEA1E", x => x.InsuranceID);
                    table.ForeignKey(
                        name: "FK__Insurance__Patie__114A936A",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID");
                });

            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    BillingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    AppointmentID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    InsuranceCoverage = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0m),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Billing__F1656D1317CDA8A3", x => x.BillingID);
                    table.ForeignKey(
                        name: "FK__Billing__Appoint__17F790F9",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID");
                    table.ForeignKey(
                        name: "FK__Billing__Patient__17036CC0",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID");
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    AppointmentID = table.Column<int>(type: "int", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LabTests__8CC33100C43F40D0", x => x.TestID);
                    table.ForeignKey(
                        name: "FK__LabTests__Appoin__0A9D95DB",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID");
                    table.ForeignKey(
                        name: "FK__LabTests__Patien__09A971A2",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID");
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentID = table.Column<int>(type: "int", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MedicalR__FBDF78C9408CB1F9", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK__MedicalRe__Appoi__52593CB8",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID");
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordID = table.Column<int>(type: "int", nullable: false),
                    MedicalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    MedicineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prescrip__40130812F4CDA569", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "MedicineId");
                    table.ForeignKey(
                        name: "FK__Prescript__Recor__5812160E",
                        column: x => x.RecordID,
                        principalTable: "MedicalRecords",
                        principalColumn: "RecordID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13045f24-6551-4562-9e48-4c51c41e0afb", null, "Staff", "STAFF" },
                    { "5c977cbd-03ae-47cf-9aa5-dbfc8f24975a", null, "Technical_Support", "TECHNICAL_SUPPORT" },
                    { "5e1a1e4b-f86e-4141-a01b-0aa888ff371e", null, "Patient", "PATIENT" },
                    { "66c5da7a-30c5-44ac-a872-6f43d91864e9", null, "Nurse", "NURSE" },
                    { "95333524-c1ac-458b-9f9a-10d70237f1ea", null, "Doctor", "DOCTOR" },
                    { "cc85a857-0008-4d5f-adfd-647de0a6d19f", null, "Admin", "ADMIN" },
                    { "f94138e6-b2f5-4fc0-a515-d3feb6680caa", null, "Receptionist", "RECEPTIONIST" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "MedicineId", "CreatedBy", "CreatedDate", "DefaultDosage", "Description", "ForAdult", "ForChildren", "IsDeleted", "Name", "Type", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4817), "500mg", "For mild to moderate pain and fever", true, true, false, "Paracetamol", "Tablet", null, null },
                    { 2, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4869), "400mg", "NSAID for pain/inflammation", true, true, false, "Ibuprofen", "Tablet", null, null },
                    { 3, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4874), "81mg", "Pain relief and antiplatelet", true, false, false, "Aspirin", "Tablet", null, null },
                    { 4, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4877), "220mg", "Long-lasting NSAID", true, false, false, "Naproxen", "Tablet", null, null },
                    { 5, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4881), "50mg", "Opioid for moderate-severe pain", true, false, false, "Tramadol", "Capsule", null, null },
                    { 6, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4898), "500mg", "Penicillin-type antibiotic", true, true, false, "Amoxicillin", "Capsule", null, null },
                    { 7, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4974), "250mg", "Macrolide antibiotic", true, true, false, "Azithromycin", "Tablet", null, null },
                    { 8, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4980), "500mg", "Fluoroquinolone antibiotic", true, false, false, "Ciprofloxacin", "Tablet", null, null },
                    { 9, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4983), "100mg", "Tetracycline antibiotic", true, false, false, "Doxycycline", "Capsule", null, null },
                    { 10, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4987), "400mg", "For anaerobic infections", true, true, false, "Metronidazole", "Tablet", null, null },
                    { 11, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4992), "10mg", "Non-drowsy allergy relief", true, true, false, "Loratadine", "Tablet", null, null },
                    { 12, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(4998), "10mg", "24-hour allergy relief", true, true, false, "Cetirizine", "Tablet", null, null },
                    { 13, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5002), "180mg", "Non-sedating antihistamine", true, false, false, "Fexofenadine", "Tablet", null, null },
                    { 14, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5005), "25mg", "For allergies and sleep aid", true, true, false, "Diphenhydramine", "Capsule", null, null },
                    { 15, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5009), "4mg/5ml", "Liquid antihistamine", true, true, false, "Chlorpheniramine", "Syrup", null, null },
                    { 16, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5026), "20mg", "Proton pump inhibitor", true, true, false, "Omeprazole", "Capsule", null, null },
                    { 17, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5030), "150mg", "H2 blocker for heartburn", true, true, false, "Ranitidine", "Tablet", null, null },
                    { 18, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5034), "500mg", "Fast-acting antacid", true, true, false, "Calcium Carbonate", "Chewable", null, null },
                    { 19, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5038), "400mg/5ml", "Liquid antacid", true, true, false, "Magnesium Hydroxide", "Liquid", null, null },
                    { 20, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5042), "20mg", "Acid reducer", true, true, false, "Famotidine", "Tablet", null, null },
                    { 21, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5045), "50mg", "SSRI antidepressant", true, false, false, "Sertraline", "Tablet", null, null },
                    { 22, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5050), "20mg", "SSRI for depression/OCD", true, true, false, "Fluoxetine", "Capsule", null, null },
                    { 23, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5053), "75mg", "SNRI antidepressant", true, false, false, "Venlafaxine", "Tablet", null, null },
                    { 24, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5057), "150mg", "Atypical antidepressant", true, false, false, "Bupropion", "Tablet", null, null },
                    { 25, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5060), "10mg", "SSRI for anxiety/depression", true, false, false, "Escitalopram", "Tablet", null, null },
                    { 26, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5064), "500mg", "Type 2 diabetes management", true, false, false, "Metformin", "Tablet", null, null },
                    { 27, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5068), "100 units/ml", "Long-acting insulin", true, true, false, "Insulin Glargine", "Injection", null, null },
                    { 28, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5071), "80mg", "Sulfonylurea for diabetes", true, false, false, "Gliclazide", "Tablet", null, null },
                    { 29, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5075), "10mg", "SGLT2 inhibitor", true, false, false, "Empagliflozin", "Tablet", null, null },
                    { 30, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5085), "1.2mg", "GLP-1 receptor agonist", true, false, false, "Liraglutide", "Injection", null, null },
                    { 31, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5120), "100mcg/dose", "Relief of asthma symptoms", true, true, false, "Salbutamol", "Inhaler", null, null },
                    { 32, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5124), "0.5mg/ml", "For COPD", true, true, false, "Ipratropium", "Nebulizer", null, null },
                    { 33, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5128), "12mcg/dose", "Long-acting bronchodilator", true, false, false, "Formoterol", "Inhaler", null, null },
                    { 34, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5133), "200mg", "For chronic asthma", true, false, false, "Theophylline", "Tablet", null, null },
                    { 35, null, new DateTime(2025, 4, 19, 22, 25, 20, 52, DateTimeKind.Local).AddTicks(5137), "5mg", "Leukotriene receptor antagonist", true, true, false, "Montelukast", "Chewable", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClinicID",
                table: "Appointments",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DateTime",
                table: "Appointments",
                column: "AppointmentDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorID",
                table: "Appointments",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_AppointmentID",
                table: "Billing",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_PatientID",
                table: "Billing",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_PaymentStatus",
                table: "Billing",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "UQ__ContactI__A9D105343C41FAFD",
                table: "ContactInfo",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNote_FacilityID",
                table: "DeliveryNote",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNote_OrderID",
                table: "DeliveryNote",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_DeliveryNoteID",
                table: "DeliveryNoteDetails",
                column: "DeliveryNoteID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_ItemID",
                table: "DeliveryNoteDetails",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "UQ__Departme__D949CC3420EBFA09",
                table: "Departments",
                column: "DepartmentName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentID",
                table: "Doctors",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "UQ__Doctors__E88901663FC5012E",
                table: "Doctors",
                column: "LicenseNumber",
                unique: true,
                filter: "[LicenseNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Facility_ContactID",
                table: "Facility",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_PatientID",
                table: "Insurance",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "UQ__Insuranc__46DA01573CC12683",
                table: "Insurance",
                column: "PolicyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_FacilityID",
                table: "Inventory",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ItemName",
                table: "Inventory",
                column: "ItemName");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_AppointmentID",
                table: "LabTests",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_PatientID",
                table: "LabTests",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "UQ__MedicalR__8ECDFCA34135F362",
                table: "MedicalRecords",
                column: "AppointmentID",
                unique: true,
                filter: "[AppointmentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Name",
                table: "Medicines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ContactID",
                table: "Patients",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name",
                table: "Patients",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PrimaryDoctorID",
                table: "Patients",
                column: "PrimaryDoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicalName",
                table: "Prescriptions",
                column: "MedicalName");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_RecordID",
                table: "Prescriptions",
                column: "RecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ContactID",
                table: "Staff",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrder_FacilityID",
                table: "SupplierOrder",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrder_Status",
                table: "SupplierOrder",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrder_SupplierID",
                table: "SupplierOrder",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrderDetails_ItemID",
                table: "SupplierOrderDetails",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrderDetails_OrderID",
                table: "SupplierOrderDetails",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Billing");

            migrationBuilder.DropTable(
                name: "DeliveryNoteDetails");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "SupplierOrderDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DeliveryNote");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "SupplierOrder");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "ContactInfo");
        }
    }
}
