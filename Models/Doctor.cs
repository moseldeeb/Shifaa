using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // ?? Verification Fields ???????????????????????????????????????
        /// ??? ????? ??????? — primary verification identifier
        public string MedicalSyndicateId { get; set; }
        public string NationalId { get; set; }

        /// Stored filename only — "guid.jpg"
        /// Physical file: wwwroot/Uploads/DoctorVerification/{UserId}/
        public string SyndicateCardImageFile { get; set; }

        /// Stored filename only — "guid.pdf"
        public string MedicalDegreeCertificateFile { get; set; }

        // ?? Professional Info ?????????????????????????????????????????

        [Required(ErrorMessage = "Specialty is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Specialty must be between 2 and 100 characters")]
        public string Specialty { get; set; }

        [Range(0, 70, ErrorMessage = "Years of experience must be between 0 and 70")]
        public int YearsOfExperience { get; set; }

        [StringLength(2000, ErrorMessage = "Bio cannot exceed 2000 characters")]
        public string? Bio { get; set; }

        public bool IsAvailable { get; set; } = false;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double Rating { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Total ratings must be a positive number")]
        public int TotalRatings { get; set; } = 0;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;


        // Navigation
        public ICollection<DoctorAssignment> Assignments { get; set; }
            = new List<DoctorAssignment>();
        public ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
