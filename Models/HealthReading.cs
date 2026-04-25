using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class HealthReading
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required(ErrorMessage = "Reading type is required")]
        public ReadingType ReadingType { get; set; } // BP, Glucose, Temperature, etc.

        [Required(ErrorMessage = "Value is required")]
        [Range(0.01, 9999.99, ErrorMessage = "Value must be between 0.01 and 9999.99")]
        public decimal Value { get; set; } // Decimal for accurate health readings (e.g., 120.5, 95.2)

        [Required(ErrorMessage = "Unit is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Unit must be between 1 and 50 characters")]
        public string Unit { get; set; } // "mmHg", "mg/dL", "°C", etc.

        [Required(ErrorMessage = "Source is required")]
        public ReadingSource Source { get; set; } // Manual, Wearable, Lab, etc.

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public AIAlert AIAlert { get; set; }
    }
}
