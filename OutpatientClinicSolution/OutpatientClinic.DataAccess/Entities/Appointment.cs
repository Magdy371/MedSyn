using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    [ForeignKey("Patient")]
    public int PatientId { get; set; }

    public int? DoctorId { get; set; }
    public int DepartmentId { get; set; }

    public int ClinicId { get; set; }

    public DateTime AppointmentDateTime { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual Clinic Clinic { get; set; } = null!;

    public virtual Doctor? Doctor { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    public virtual MedicalRecord? MedicalRecord { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
