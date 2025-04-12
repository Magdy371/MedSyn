namespace OutpatientClinic.Presentation.Models
{
    public class MedicalRecordIndexViewModel
    {
        public int RecordId { get; set; }
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatorRole { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
