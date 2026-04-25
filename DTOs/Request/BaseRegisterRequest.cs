using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.DTOs.Request
{
    public class BaseRegisterRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
    public class MemberRegisterRequest : BaseRegisterRequest
    {
        // Nothing extra — FirstName, LastName, Email, Password only
    }
    public class CaregiverRegisterRequest : BaseRegisterRequest
    {
        // Nothing extra — FirstName, LastName, Email, Password only
    }
    // Doctor — needs more info for Admin to review the request
    public class DoctorRegisterRequest : BaseRegisterRequest
    {
        // ── Identity Verification ─────────────────────────────────────

        /// <summary>
        /// رقم نقابة الأطباء المصريين — unique per doctor, required for approval
        /// </summary>
        [Required(ErrorMessage = "Medical Syndicate ID is required")]
        [StringLength(50)]
        public string MedicalSyndicateId { get; set; }

        /// <summary>
        /// الرقم القومي — cross-referenced with syndicate ID by Admin
        /// </summary>
        [Required(ErrorMessage = "National ID is required")]
        [StringLength(14, MinimumLength = 14,
         ErrorMessage = "National ID must be exactly 14 digits")]
        public string NationalId { get; set; }

        // ── Document Uploads ──────────────────────────────────────────

        /// <summary>
        /// Scanned syndicate card — front face (صورة كارنيه النقابة)
        /// Stored as filename in wwwroot/Uploads/DoctorVerification/{userId}/
        /// </summary>
        [Required(ErrorMessage = "Syndicate card image is required")]
        public IFormFile SyndicateCardImage { get; set; }

        /// <summary>
        /// Medical degree or specialty certificate scan
        /// </summary>
        [Required(ErrorMessage = "Medical degree certificate is required")]
        public IFormFile MedicalDegreeCertificate { get; set; }

        // ── Professional Info ─────────────────────────────────────────

        [Required(ErrorMessage = "Specialty is required")]
        [StringLength(100)]
        public string Specialty { get; set; }

        public int YearsOfExperience { get; set; }


        [StringLength(2000)]
        public string? Bio { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    // Medical Center — needs facility details for Admin to verify
    public class MedicalCenterRegisterRequest : BaseRegisterRequest
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Facility name is required")]
        [StringLength(200)]
        public string FacilityName { get; set; }

        [Required]
        public FacilityType FacilityType { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [StringLength(500)]
        public string? WorkingHours { get; set; }
    }
}
