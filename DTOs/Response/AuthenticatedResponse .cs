using static Shifaa.Models.Enums;

namespace Shifaa.DTOs.Response
{
    public class AuthenticatedResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UserType SelectedRole { get; set; }
        public List<UserType> AvailableRoles { get; set; } = new();
        public string RedirectUrl { get; set; } = string.Empty;
    }
}
