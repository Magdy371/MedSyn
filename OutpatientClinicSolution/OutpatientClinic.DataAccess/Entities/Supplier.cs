using System;
using System.Collections.Generic;

namespace OutpatientClinic.DataAccess.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<SupplierOrder> SupplierOrders { get; set; } = new List<SupplierOrder>();
}
