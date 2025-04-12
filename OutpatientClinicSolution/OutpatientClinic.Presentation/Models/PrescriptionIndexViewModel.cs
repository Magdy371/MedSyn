namespace OutpatientClinic.Presentation.Models
{
    public class PrescriptionIndexViewModel
    {
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public string PatientName { get; set; }
        public string MedicalName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatorRole { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
