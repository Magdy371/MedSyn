﻿using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class DeliveryNote
{
    public int DeliveryNoteId { get; set; }

    public int? OrderId { get; set; }

    public int? FacilityId { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public string? ReceivedBy { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<DeliveryNoteDetail> DeliveryNoteDetails { get; set; } = new List<DeliveryNoteDetail>();

    public virtual Facility? Facility { get; set; }

    public virtual SupplierOrder? Order { get; set; }
}
