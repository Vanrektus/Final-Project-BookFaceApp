using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Models.Publication
{
    public class PublicationAddCommentModel
    {
        [Required]
        [MaxLength(MaxCommentText)]
        public string Text { get; set; } = null!;

        public int PublicationId { get; set; }
    }
}
