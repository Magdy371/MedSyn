using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class DeliveryNoteDetail
{
    public int DeliveryDetailId { get; set; }

    public int? DeliveryNoteId { get; set; }

    public int? ItemId { get; set; }

    public int QuantityDelivered { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual DeliveryNote? DeliveryNote { get; set; }

    public virtual Inventory? Item { get; set; }
}
