using System.ComponentModel.DataAnnotations;
namespace OutpatientClinic.Presentation.Models
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Staff member is required")]
        [Display(Name = "Staff Member")]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Specialty is required")]
        [StringLength(100, ErrorMessage = "Specialty cannot exceed 100 characters")]
        public string Specialty { get; set; } = string.Empty;

        [Display(Name = "License Number")]
        [StringLength(50, ErrorMessage = "License number cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "License number should contain only uppercase letters, numbers, and hyphens")]
        public string? LicenseNumber { get; set; }

        // Additional properties for display purposes
        [Display(Name = "First Name")]
        public string? StaffFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? StaffLastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{StaffFirstName} {StaffLastName}";

        [Display(Name = "Department Name")]
        public string? DepartmentName { get; set; }
    }
}
