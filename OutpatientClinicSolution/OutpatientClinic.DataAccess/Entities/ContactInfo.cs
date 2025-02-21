using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class ContactInfo
{
    public int ContactId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
