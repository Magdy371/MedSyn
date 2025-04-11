namespace OutpatientClinic.Presentation.Models
{
    public class LabTestIndexViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; } = null!;
        public DateTime TestDate { get; set; }
        public string PatientName { get; set; } = null!;
        public int? AppointmentId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
