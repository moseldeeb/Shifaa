using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required(ErrorMessage = "Doctor ID is required")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Medical center ID is required")]
        public int MedicalCenterId { get; set; }
        public MedicalCenter MedicalCenter { get; set; }

        [Required(ErrorMessage = "Time slot ID is required")]
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [StringLength(2000, ErrorMessage = "Symptom summary cannot exceed 2000 characters")]
        public string? SymptomSummary { get; set; }

        // Display only — no payment processing in Phase 1
        public decimal AppointmentFee { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;


        // Populated when Medical Center reschedules
        public DateTime? RescheduledTo { get; set; }
        public string? RescheduleReason { get; set; }
        // Navigation
        public Prescription? Prescription { get; set; }
    }
}
