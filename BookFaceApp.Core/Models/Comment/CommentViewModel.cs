namespace BookFaceApp.Core.Models.Comment
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Text { get; set; } = null!;

		public bool IsDeleted { get; set; } = false;

		public string UserId { get; set; } = null!;

		public Infrastructure.Data.Entities.User User { get; set; } = null!;
	}
}
