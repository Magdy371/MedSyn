using System;
using System.ComponentModel.DataAnnotations;

namespace OutpatientClinic.Presentation.Models
{
    public class AppointmentViewModel
    {
        [Required]
        public string? UserId { get; set; } // For existing patients
        [Required]
        public string? PatientName { get; set; } // For new patients
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public DateTime AppointmentDateTime { get; set; }
        public int? ClinicId { get; set; } // Optional for emergency fallback
        [Display(Name = "Appointment Status")]
        public string Status { get; set; } = "Pending";
    }
}