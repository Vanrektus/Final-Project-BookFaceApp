using BookFaceApp.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.PublicationConstants;

namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxPublicationTitle)]
        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public string? UserId { get; set; }
    }
}
