using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class GuardianMember
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Caregiver ID is required")]
        public int CaregiverId { get; set; }
        public Caregiver Caregiver { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required(ErrorMessage = "Link status is required")]
        public LinkStatus Status { get; set; } = LinkStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AcceptedAt { get; set; }
        public DateTime? RejectedAt { get; set; }

       
    }

   
}
