﻿using System.ComponentModel.DataAnnotations;
namespace OutpatientClinic.Presentation.Models
{
    public class DoctorViewModel
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Specialty { get; set; }
        public string? DepartmentName { get; set; }
    }
}
