using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Insurance
{
    public int InsuranceId { get; set; }

    public int PatientId { get; set; }

    public string Provider { get; set; } = null!;

    public string PolicyNumber { get; set; } = null!;

    public DateOnly ExpiryDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
