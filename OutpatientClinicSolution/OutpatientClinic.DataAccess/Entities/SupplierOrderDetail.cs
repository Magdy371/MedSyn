using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class SupplierOrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Inventory? Item { get; set; }

    public virtual SupplierOrder? Order { get; set; }
}
