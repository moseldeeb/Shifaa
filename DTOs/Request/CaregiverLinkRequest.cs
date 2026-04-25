using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class CaregiverLinkRequest
    {
        [Required]
        [EmailAddress]
        public string MemberEmail { get; set; }

        // e.g. "Father", "Son", "Nurse", "Friend"
        [StringLength(100)]
        public string? RelationshipType { get; set; }
    }
}
