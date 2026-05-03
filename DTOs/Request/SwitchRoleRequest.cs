using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class SwitchRoleRequest
    {
        [Required(ErrorMessage = "Role is required")]
        public Role NewRole { get; set; }
    }
}
