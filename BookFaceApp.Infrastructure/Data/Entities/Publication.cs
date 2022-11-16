using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookFaceApp.Infrastructure.Data.DataConstants.PublicationConstants;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class Publication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxPublicationTitle)]
        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }

		[Required]
        public bool IsDeleted { get; set; } = false;

		[Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public int? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group? Group { get; set; }

        public List<PublicationComment> PublicationsComments { get; set; } = new List<PublicationComment>();

        public List<UserPublication> UsersPublications { get; set; } = new List<UserPublication>();
    }
}
