using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class PrescriptionItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Prescription ID is required")]
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        [Required(ErrorMessage = "Medicine name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Medicine name must be between 1 and 200 characters")]
        public string MedicineName { get; set; }

        [Required(ErrorMessage = "Dose is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Dose must be between 1 and 100 characters")]
        public string Dose { get; set; } // "500mg", "10ml", etc.

        [Required(ErrorMessage = "Frequency is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Frequency must be between 1 and 200 characters")]
        public string Frequency { get; set; } // "Twice daily", "Once at night", etc.

        [StringLength(100, ErrorMessage = "Meal timing cannot exceed 100 characters")]
        public MealTiming MealTiming { get; set; } // "Before meal", "After meal", "With meal"

        public PrescriptionDays Days { get; set; } // Monday, Tuesday, etc.

        public TimeOnly? StartTime { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int Duration { get; set; } // Days
    }
}
