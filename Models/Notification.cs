using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Notification type is required")]
        public NotificationType Type { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 2000 characters")]
        public string Message { get; set; }

        [StringLength(500, ErrorMessage = "Title cannot exceed 500 characters")]
        public string? Title { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // For actionable notifications like CaregiverLinkRequest
        // Stores JSON payload e.g. { "requestId": 5 }
        public string? ActionPayload { get; set; }
        public bool IsActioned { get; set; } = false;

        // Links notification to source entity
        public int? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; } // "GuardianMember", "DoctorAssignment", etc.
    }
}
