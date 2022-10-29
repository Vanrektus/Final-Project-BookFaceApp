using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookFaceApp.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCommentText)]
        public string Text { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public List<PublicationComment> PublicationsComments = new List<PublicationComment>();
    }
}
