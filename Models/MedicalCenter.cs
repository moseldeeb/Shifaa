using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class MedicalCenter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Facility name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Facility name must be between 2 and 200 characters")]
        public string FacilityName { get; set; }

        [Required(ErrorMessage = "Facility type is required")]
        public FacilityType FacilityType { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string? Address { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? Longitude { get; set; }

        [StringLength(500, ErrorMessage = "Working hours cannot exceed 500 characters")]
        public string? WorkingHours { get; set; }

        [Phone(ErrorMessage = "Phone number format is invalid")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? PhoneNumber { get; set; }

        [StringLength(500, ErrorMessage = "Website URL cannot exceed 500 characters")]
        public string? Website { get; set; }
        public DateTime? CreatedAt { get; set; }

        public bool IsVerified { get; set; } = false; // Admin approves

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double Rating { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Total ratings must be a positive number")]
        public int TotalRatings { get; set; } = 0;



        // Navigation
        public ICollection<DoctorAssignment> DoctorAssignments { get; set; }
           = new List<DoctorAssignment>();
        public ICollection<DoctorSchedule> Schedules { get; set; }
            = new List<DoctorSchedule>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    
}
