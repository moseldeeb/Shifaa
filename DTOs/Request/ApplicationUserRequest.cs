using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class ApplicationUserRequest
    {
        public string? FullName { get; set; } 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

    }
}
