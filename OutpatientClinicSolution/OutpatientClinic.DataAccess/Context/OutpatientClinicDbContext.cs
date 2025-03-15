using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutpatientClinic.DataAccess.Entities;

namespace OutpatientClinic.DataAccess.Context;

public partial class OutpatientClinicDbContext : IdentityDbContext<ApplicationUser>
{
    public OutpatientClinicDbContext()
    {
    }

    public OutpatientClinicDbContext(DbContextOptions<OutpatientClinicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<DeliveryNote> DeliveryNotes { get; set; }

    public virtual DbSet<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierOrder> SupplierOrders { get; set; }

    public virtual DbSet<SupplierOrderDetail> SupplierOrderDetails { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NAUIN65\\DEPIR2_2024;Initial Catalog=OutpatientClinic;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2E6C3C030");

            entity.HasIndex(e => e.AppointmentDateTime, "IX_Appointments_DateTime");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Clini__4AB81AF0");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__49C3F6B7");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Appointme__Patie__48CFD27E");
        });

        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("PK__Billing__F1656D1317CDA8A3");

            entity.ToTable("Billing");

            entity.HasIndex(e => e.AppointmentId, "IX_Billing_AppointmentID");

            entity.HasIndex(e => e.PatientId, "IX_Billing_PatientID");

            entity.HasIndex(e => e.PaymentStatus, "IX_Billing_PaymentStatus");

            entity.Property(e => e.BillingId).HasColumnName("BillingID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InsuranceCoverage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Billings)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Billing__Appoint__17F790F9");

            entity.HasOne(d => d.Patient).WithMany(p => p.Billings)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Billing__Patient__17036CC0");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinics__3347C2FDCF7CE5D9");

            entity.Property(e => e.ClinicId).HasColumnName("ClinicID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ClinicName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__ContactI__5C6625BBA84CC8E1");

            entity.ToTable("ContactInfo");

            entity.HasIndex(e => e.Email, "UQ__ContactI__A9D105343C41FAFD").IsUnique();

            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DeliveryNote>(entity =>
        {
            entity.HasKey(e => e.DeliveryNoteId).HasName("PK__Delivery__2A1CDD7E87E9E1FA");

            entity.ToTable("DeliveryNote");

            entity.HasIndex(e => e.OrderId, "IX_DeliveryNote_OrderID");

            entity.Property(e => e.DeliveryNoteId).HasColumnName("DeliveryNoteID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ReceivedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Facility).WithMany(p => p.DeliveryNotes)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("FK__DeliveryN__Facil__7D439ABD");

            entity.HasOne(d => d.Order).WithMany(p => p.DeliveryNotes)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__DeliveryN__Order__7C4F7684");
        });

        modelBuilder.Entity<DeliveryNoteDetail>(entity =>
        {
            entity.HasKey(e => e.DeliveryDetailId).HasName("PK__Delivery__EFD2C287E16C026E");

            entity.Property(e => e.DeliveryDetailId).HasColumnName("DeliveryDetailID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveryNoteId).HasColumnName("DeliveryNoteID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DeliveryNote).WithMany(p => p.DeliveryNoteDetails)
                .HasForeignKey(d => d.DeliveryNoteId)
                .HasConstraintName("FK__DeliveryN__Deliv__02FC7413");

            entity.HasOne(d => d.Item).WithMany(p => p.DeliveryNoteDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__DeliveryN__ItemI__03F0984C");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD3678F47F");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC3420EBFA09").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EDFCF437B0A");

            entity.HasIndex(e => e.DepartmentId, "IX_Doctors_DepartmentID");

            entity.HasIndex(e => e.LicenseNumber, "UQ__Doctors__E88901663FC5012E").IsUnique();

            entity.Property(e => e.DoctorId)
                .ValueGeneratedNever()
                .HasColumnName("DoctorID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.Specialty).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__Departm__3E52440B");

            entity.HasOne(d => d.DoctorNavigation).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__DoctorI__3D5E1FD2");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.FacilityId).HasName("PK__Facility__5FB08B94472946F9");

            entity.ToTable("Facility");

            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FacilityName).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Contact).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK__Facility__Contac__5DCAEF64");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__Insuranc__74231BC423DEEA1E");

            entity.ToTable("Insurance");

            entity.HasIndex(e => e.PatientId, "IX_Insurance_PatientID");

            entity.HasIndex(e => e.PolicyNumber, "UQ__Insuranc__46DA01573CC12683").IsUnique();

            entity.Property(e => e.InsuranceId).HasColumnName("InsuranceID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PolicyNumber).HasMaxLength(50);
            entity.Property(e => e.Provider).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Patient).WithMany(p => p.Insurances)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Insurance__Patie__114A936A");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Inventor__727E83EB1BE9BB16");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.ItemName, "IX_Inventory_ItemName");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Facility).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("FK__Inventory__Facil__6383C8BA");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__LabTests__8CC33100C43F40D0");

            entity.HasIndex(e => e.AppointmentId, "IX_LabTests_AppointmentID");

            entity.HasIndex(e => e.PatientId, "IX_LabTests_PatientID");

            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.TestName).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__LabTests__Appoin__0A9D95DB");

            entity.HasOne(d => d.Patient).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTests__Patien__09A971A2");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MedicalR__FBDF78C9408CB1F9");

            entity.HasIndex(e => e.AppointmentId, "UQ__MedicalR__8ECDFCA34135F362").IsUnique();

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithOne(p => p.MedicalRecord)
                .HasForeignKey<MedicalRecord>(d => d.AppointmentId)
                .HasConstraintName("FK__MedicalRe__Appoi__52593CB8");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC34675504678");

            entity.HasIndex(e => e.ContactId, "IX_Patients_ContactID");

            entity.HasIndex(e => new { e.LastName, e.FirstName }, "IX_Patients_Name");

            entity.HasIndex(e => e.PrimaryDoctorId, "IX_Patients_PrimaryDoctorID");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PrimaryDoctorId).HasColumnName("PrimaryDoctorID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Contact).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK__Patients__Contac__4222D4EF");

            entity.HasOne(d => d.PrimaryDoctor).WithMany(p => p.Patients)
                .HasForeignKey(d => d.PrimaryDoctorId)
                .HasConstraintName("FK__Patients__Primar__4316F928");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130812F4CDA569");

            entity.HasIndex(e => e.MedicalName, "IX_Prescriptions_MedicalName");

            entity.Property(e => e.PrescriptionId).HasColumnName("PrescriptionID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dosage).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MedicalName).HasMaxLength(100);
            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Record).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescript__Recor__5812160E");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF719817DB0");

            entity.HasIndex(e => e.ContactId, "IX_Staff_ContactID");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Contact).WithMany(p => p.Staff)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK__Staff__ContactID__36B12243");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694F74E0005");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<SupplierOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Supplier__C3905BAF90D0822A");

            entity.ToTable("SupplierOrder");

            entity.HasIndex(e => e.Status, "IX_SupplierOrder_Status");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Facility).WithMany(p => p.SupplierOrders)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("FK__SupplierO__Facil__6EF57B66");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierOrders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__SupplierO__Suppl__6E01572D");
        });

        modelBuilder.Entity<SupplierOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__Supplier__D3B9D30C62611F41");

            entity.HasIndex(e => e.OrderId, "IX_SupplierOrderDetails_OrderID");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.SupplierOrderDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__SupplierO__ItemI__76969D2E");

            entity.HasOne(d => d.Order).WithMany(p => p.SupplierOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__SupplierO__Order__75A278F5");
        });

        OnModelCreatingPartial(modelBuilder);

        base.OnModelCreating(modelBuilder);

        // Seeding roles
        modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().HasData(
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Doctor", NormalizedName = "DOCTOR" },
            new IdentityRole { Name = "Patient", NormalizedName = "PATIENT" });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
