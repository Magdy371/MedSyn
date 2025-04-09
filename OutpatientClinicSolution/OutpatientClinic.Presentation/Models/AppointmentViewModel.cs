using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OutpatientClinic.Presentation.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public int? ClinicId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public string? PatientSearch { get; set; }
    }
}
