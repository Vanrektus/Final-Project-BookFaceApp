using BookFaceApp.Models.Comment;
using BookFaceApp.Models.Publication;

namespace BookFaceApp.Contracts
{
	public interface ICommentService
	{
		Task AddCommentAsync(CommentAddModel model, int publicationId, string userId);

		Task<CommentEditModel> GetCommentForEditAsync(int publicationId);

		Task EditCommentAsync(CommentEditModel model);

		Task DeleteCommentAsync(int publicationId, string userId);
	}
}
