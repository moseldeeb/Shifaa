using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class DoctorAssignment
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int MedicalCenterId { get; set; }
        public MedicalCenter MedicalCenter { get; set; }

        public DoctorAssignmentStatus Status { get; set; }
            = DoctorAssignmentStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AcceptedAt { get; set; }
        public DateTime? TerminatedAt { get; set; }
    }
}
