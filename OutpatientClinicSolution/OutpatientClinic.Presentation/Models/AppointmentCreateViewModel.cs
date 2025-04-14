using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OutpatientClinic.Presentation.Models
{
    public class AppointmentCreateViewModel
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? DoctorId { get; set; }

        [Required]
        public int ClinicId { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public string? Notes { get; set; }
    }

}
