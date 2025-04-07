// ViewModels/AppointmentCreateViewModel.cs
using OutpatientClinic.DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace OutpatientClinic.Presentation.Models
{
    public class AppointmentCreateViewModel
    {
        public Appointment Appointment { get; set; } = new Appointment();

        [Display(Name = "Search Patient")]
        public string? PatientSearchTerm { get; set; }

        public int? SelectedPatientId { get; set; }

        [EmailAddress]
        [Display(Name = "New Patient Email")]
        public string? NewPatientEmail { get; set; }

        [Display(Name = "First Name")]
        public string? NewPatientFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? NewPatientLastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateOnly NewPatientDob { get; set; }

        public int? ClinicId { get; set; }
    }
}