using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Core.Models.Comment
{
	public class CommentAddModel
    {
        [Required]
        [StringLength(MaxCommentText, MinimumLength = MinCommentText)]
        public string Text { get; set; } = null!;

        public int PublicationId { get; set; }
    }
}
