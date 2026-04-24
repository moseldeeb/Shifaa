using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required(ErrorMessage = "Target ID is required")]
        public int TargetId { get; set; } // Id of Doctor or MedicalCenter

        [Required(ErrorMessage = "Target type is required")]
        public TargetType TargetType { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation (only one populated based on TargetType)
        public Doctor? Doctor { get; set; }
        public MedicalCenter? MedicalCenter { get; set; }
    }

    
}
