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

        [StringLength(100, ErrorMessage = "Relationship type cannot exceed 100 characters")]
        public string? RelationshipType { get; set; } // "Father", "Son", "Nurse" etc
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<GuardianMember> Members { get; set; } = new List<GuardianMember>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
