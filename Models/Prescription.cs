using System.ComponentModel.DataAnnotations;

namespace Shifaa.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doctor ID is required")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        // Nullable — prescription can exist without a formal appointment
        public int? AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        // Navigation
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}
