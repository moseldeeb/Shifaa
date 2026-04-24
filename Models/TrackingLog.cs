using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class TrackingLog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
