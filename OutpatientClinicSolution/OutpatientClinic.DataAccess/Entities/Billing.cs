using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Billing
{
    public int BillingId { get; set; }

    public int PatientId { get; set; }

    public int? AppointmentId { get; set; }

    public decimal Amount { get; set; }

    public decimal? InsuranceCoverage { get; set; }

    public string? PaymentStatus { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
