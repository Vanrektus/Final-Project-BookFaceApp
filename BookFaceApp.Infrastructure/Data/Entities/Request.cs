using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.RequestConstants;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxRequestName)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(MaxRequestStatus)]
        public string Status { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
