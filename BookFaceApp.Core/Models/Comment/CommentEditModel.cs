using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.CommentConstants;

namespace BookFaceApp.Core.Models.Comment
{
	public class CommentEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxCommentText, MinimumLength = MinCommentText)]
        public string Text { get; set; } = null!;

		public string? UserId { get; set; }

		public int Publicationid { get; set; }
    }
}
