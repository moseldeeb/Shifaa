using System.ComponentModel.DataAnnotations;
using static Shifaa.Models.Enums;

namespace Shifaa.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Member ID is required")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required(ErrorMessage = "Uploaded by is required")]
        [StringLength(450, ErrorMessage = "Uploaded by cannot exceed 450 characters")]
        public string UploadedBy { get; set; } // UserId of the uploader (Member or Doctor)
        public ApplicationUser Uploader { get; set; }
        [Required(ErrorMessage = "Source type is required")]
        public SourceType SourceType { get; set; } // Where the record came from (Member, Doctor, etc.)
        
        // Stores only the file name + extension e.g. "3f92a1b4-ecb7.pdf"
        // Physical file saved to wwwroot/MedicalRecords/Members/{MemberId}/
        [Required(ErrorMessage = "File is required")]
        [StringLength(260, ErrorMessage = "File name cannot exceed 260 characters")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "File type is required")]
        public FileType FileType { get; set; } // PDF or Image

        [Required(ErrorMessage = "Title is required")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 300 characters")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
