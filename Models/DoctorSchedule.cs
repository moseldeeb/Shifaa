using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doctor ID is required")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // Which facility this schedule belongs to
        public int MedicalCenterId { get; set; }
        public MedicalCenter MedicalCenter { get; set; }

        [Required(ErrorMessage = "Day is required")]
        public DayOfWeek Day { get; set; } // Monday, Tuesday, etc.

        [Required(ErrorMessage = "Start time is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeOnly EndTime { get; set; }

        
        // Navigation
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }
}
