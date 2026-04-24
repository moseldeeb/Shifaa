using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doctor schedule ID is required")]
        public int DoctorScheduleId { get; set; }
        public DoctorSchedule DoctorSchedule { get; set; }

        [Required(ErrorMessage = "Slot date is required")]
        public DateOnly SlotDate { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeOnly EndTime { get; set; }

        public bool IsBooked { get; set; } = false;

        

        // Navigation
        public Appointment Appointment { get; set; }
    }
}
