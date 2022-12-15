using BookFaceApp.Core.Contracts;
using BookFaceApp.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.PublicationConstants;

namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationEditModel : IPublicationModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxPublicationTitle, MinimumLength = MinPublicationTitle)]
        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public int? GroupId { get; set; }

        public string? UserId { get; set; }
    }
}
