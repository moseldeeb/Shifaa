using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Gender cannot exceed 20 characters")]
        public string? Gender { get; set; }        // "Male" / "Female"

        [StringLength(10, ErrorMessage = "Blood type cannot exceed 10 characters")]
        public string? BloodType { get; set; }     // "O+" etc

        [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 500 kg")]
        public decimal? Weight { get; set; }       // kg

        [Range(1, 300, ErrorMessage = "Height must be between 1 and 300 cm")]
        public decimal? Height { get; set; }       // cm

        [StringLength(1000, ErrorMessage = "Chronic conditions cannot exceed 1000 characters")]
        public string? ChronicConditions { get; set; }

        [StringLength(1000, ErrorMessage = "Drug allergies cannot exceed 1000 characters")]
        public string? DrugAllergies { get; set; }

        [StringLength(1000, ErrorMessage = "Food allergies cannot exceed 1000 characters")]
        public string? FoodAllergies { get; set; }

        [StringLength(200, ErrorMessage = "Insurance provider cannot exceed 200 characters")]
        public string? InsuranceProvider { get; set; }

        [StringLength(100, ErrorMessage = "Insurance ID cannot exceed 100 characters")]
        public string? InsuranceId { get; set; }

        // GPS tracking
        public bool GpsEnabled { get; set; } = false;

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? LastLatitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? LastLongitude { get; set; }

        public DateTime? LastLocationUpdate { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<HealthReading> HealthReadings { get; set; } = new List<HealthReading>();
        public ICollection<AIAlert> AIAlerts { get; set; } = new List<AIAlert>();
        public ICollection<TrackingLog> TrackingLogs { get; set; } = new List<TrackingLog>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<GuardianMember> Caregivers { get; set; } = new List<GuardianMember>();
    }
}
