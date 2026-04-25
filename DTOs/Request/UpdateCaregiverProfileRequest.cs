using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class UpdateCaregiverProfileRequest
    {
        [Phone]
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Language { get; set; }
    }
}
