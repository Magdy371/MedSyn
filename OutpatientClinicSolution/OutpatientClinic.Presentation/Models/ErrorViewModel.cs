namespace OutpatientClinic.Presentation.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string? Message { get; set; } // Added property to fix CS0117  
    }
}
