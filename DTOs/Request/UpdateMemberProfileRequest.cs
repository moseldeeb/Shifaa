using System.ComponentModel.DataAnnotations;

namespace Shifaa.DTOs.Request
{
    public class UpdateMemberProfileRequest
    {
        [Phone]
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodType { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public string? ChronicConditions { get; set; }
        public string? DrugAllergies { get; set; }
        public string? FoodAllergies { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsuranceId { get; set; }
        public string? Language { get; set; }
    }
}
