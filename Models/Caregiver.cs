using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class Caregiver
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Gender cannot exceed 20 characters")]
        public string? Gender { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Navigation
        public ICollection<GuardianMember> Members { get; set; } = new List<GuardianMember>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
