using System.ComponentModel.DataAnnotations;

namespace OutpatientClinic.Core.DTOs
{
    public class PatientRegisterDto
    {
        [Required] public string? Username { get; set; }
        [Required][EmailAddress] public string? Email { get; set; }
        [Required] public string? Password { get; set; }
        [Required] public string? PhoneNumber { get; set; }
        [Required] public string? Role { get; set; }

        // Patient-specific fields
        [Required] public string? FirstName { get; set; }
        [Required] public string? LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Dob { get; set; }
    }
}