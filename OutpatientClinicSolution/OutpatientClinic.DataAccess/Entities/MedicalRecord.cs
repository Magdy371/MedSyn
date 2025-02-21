using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int? AppointmentId { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string Treatment { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
