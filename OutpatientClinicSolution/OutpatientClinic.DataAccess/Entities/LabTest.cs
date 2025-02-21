using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class LabTest
{
    public int TestId { get; set; }

    public int PatientId { get; set; }

    public int? AppointmentId { get; set; }

    public string TestName { get; set; } = null!;

    public DateOnly TestDate { get; set; }

    public string? Results { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
