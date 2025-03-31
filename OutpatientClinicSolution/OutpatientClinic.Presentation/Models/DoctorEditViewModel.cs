using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OutpatientClinic.Presentation.Models
{
    public class DoctorEditViewModel
    {
        public string? UserId { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        [Required]
        public string? Specialty { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public SelectList? Departments { get; set; }
    }
}
