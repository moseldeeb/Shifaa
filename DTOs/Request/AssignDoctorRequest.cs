using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class AssignDoctorRequest
    {
        [Required]
        [EmailAddress]
        public string DoctorEmail { get; set; }

        // Fee is set by the center — not by the doctor
        [Required]
        [Range(0, 99999.99)]
        public decimal ConsultationFee { get; set; }
    }
}
