// Medicine.cs
namespace OutpatientClinic.DataAccess.Entities;

public class Medicine
{
    public int MedicineId { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!; // e.g., Tablet, Liquid, Capsule
    public string DefaultDosage { get; set; } = null!; // e.g., "500mg", "10ml"
    public bool ForAdult { get; set; }
    public bool ForChildren { get; set; }
    public string? Description { get; set; }

    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool? IsDeleted { get; set; }
}