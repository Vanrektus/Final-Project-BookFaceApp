using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Models.Comment
{
	public class CommentAddModel
    {
        [Required]
        [MaxLength(MaxCommentText)]
        public string Text { get; set; } = null!;

        public int PublicationId { get; set; }
    }
}
