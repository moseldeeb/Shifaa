using System.ComponentModel.DataAnnotations;
using Shifaa.Utilities;

namespace Shifaa.DTOs.Request
{
    public enum Role
    {
        Member = 1,
        Caregiver = 2,
        Doctor = 3,
        MedicalCenter = 4,
    }
    public class LoginRequest
    {
        public Role Role { get; set; }
        public string UserNameOrEmail { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
