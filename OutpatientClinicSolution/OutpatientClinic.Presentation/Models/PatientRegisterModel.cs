namespace OutpatientClinic.Presentation.Models
{
    public class PatientRegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
