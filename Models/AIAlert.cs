using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class AIAlert
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required(ErrorMessage = "Health reading ID is required")]
        public int ReadingId { get; set; }
        public HealthReading Reading { get; set; }

        [Required(ErrorMessage = "Alert type is required")]
        public string AlertType { get; set; }

        [Required(ErrorMessage = "Severity is required")]
        public AlertSeverity Severity { get; set; }

        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string? Message { get; set; }
        public decimal ThresholdValue { get; set; }
        public string RecommendedAction { get; set; }
        public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsAcknowledged { get; set; } = false;
        public DateTime? AcknowledgedAt { get; set; }

        // Navigation
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
