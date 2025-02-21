using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Inventory
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int? FacilityId { get; set; }

    public DateOnly? LastRestocked { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<DeliveryNoteDetail> DeliveryNoteDetails { get; set; } = new List<DeliveryNoteDetail>();

    public virtual Facility? Facility { get; set; }

    public virtual ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; } = new List<SupplierOrderDetail>();
}
