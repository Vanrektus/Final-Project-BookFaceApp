using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookFaceApp.Infrastructure.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCommentText)]
        public string Text { get; set; } = null!;

		[Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public List<PublicationComment> PublicationsComments = new List<PublicationComment>();
    }
}
