using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Shared across ALL roles
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
        public string LastName { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string? Address { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        // Stores only "guid.jpg" — not a full path or URL
        // Physical file saved to wwwroot/Images/Profiles/
        [StringLength(500, ErrorMessage = "Profile picture URL cannot exceed 2 MB")]
        public string? ProfilePicture { get; set; }

        [StringLength(10, ErrorMessage = "Language code cannot exceed 10 characters")]
        public string? Language { get; set; } = "ar";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties (only one will be non-null per user)
        public Member? Member { get; set; }
        public Caregiver? Caregiver { get; set; }
        public Doctor? Doctor { get; set; }
        public MedicalCenter? MedicalCenter { get; set; }
        // Admin has no profile table - admin data lives in ApplicationUser only

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
