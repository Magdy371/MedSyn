using OutpatientClinic.DataAccess.Entities;

namespace OutpatientClinic.Presentation.Models
{
    // PatientDashboardViewModel.cs
    public class PatientDashboardViewModel
    {
        public Patient Patient { get; set; }
        public ApplicationUser User { get; set; }
        public int LabTestsCount { get; set; }
        public int PrescriptionsCount { get; set; }
        public int AppointmentsCount { get; set; }
        public Doctor Doctor { get; set; }
    }
}
