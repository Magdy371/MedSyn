using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class SupplierOrder
{
    public int OrderId { get; set; }

    public int? SupplierId { get; set; }

    public int? FacilityId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly? ExpectedDeliveryDate { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<DeliveryNote> DeliveryNotes { get; set; } = new List<DeliveryNote>();

    public virtual Facility? Facility { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; } = new List<SupplierOrderDetail>();
}
