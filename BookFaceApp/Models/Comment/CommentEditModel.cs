using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Models.Comment
{
	public class CommentEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCommentText)]
        public string Text { get; set; } = null!;

		public string? UserId { get; set; }

		public int Publicationid { get; set; }
    }
}
