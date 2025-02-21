using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int DepartmentId { get; set; }

    public string Specialty { get; set; } = null!;

    public string? LicenseNumber { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department Department { get; set; } = null!;

    public virtual Staff DoctorNavigation { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
