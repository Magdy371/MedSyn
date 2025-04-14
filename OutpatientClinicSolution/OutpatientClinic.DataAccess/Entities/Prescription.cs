using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int RecordId { get; set; }

    public string MedicalName { get; set; } = null!;

    public string Dosage { get; set; } = null!;

    public string? Instructions { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual MedicalRecord? Record { get; set; }

    //new added
    public int? MedicineId { get; set; } // Optional foreign key
    public virtual Medicine? Medicine { get; set; }

}
