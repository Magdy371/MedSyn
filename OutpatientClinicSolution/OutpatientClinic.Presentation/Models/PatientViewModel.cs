using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace OutpatientClinic.Presentation.Models
{
    public class PatientViewModel
    {
        // Patient table fields
        public int? PatientId { get; set; }
        public DateOnly? Dob { get; set; }
        public int? PrimaryDoctorId { get; set; }

        // Identity fields (from ApplicationUser)
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Extra field to filter/select a department for PrimaryDoctor assignment.
        public string? WantedDepartment { get; set; }

        // Dropdown list for available doctors (populated in the controller)
        public IEnumerable<SelectListItem>? AvailableDoctors { get; set; }
    }
}
