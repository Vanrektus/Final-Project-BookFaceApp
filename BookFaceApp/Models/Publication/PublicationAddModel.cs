using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.PublicationConstants;

namespace BookFaceApp.Models.Publication
{
    public class PublicationAddModel
    {
        [Required]
        [MaxLength(MaxPublicationTitle)]
        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
