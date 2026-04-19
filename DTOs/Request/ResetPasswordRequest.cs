using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class ResetPasswordRequest
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [DataType(DataType.Password) , Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
