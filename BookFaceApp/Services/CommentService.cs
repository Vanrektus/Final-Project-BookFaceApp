using BookFaceApp.Contracts;
using BookFaceApp.Data.Common;
using BookFaceApp.Data.Entities;
using BookFaceApp.Models.Comment;
using BookFaceApp.Models.Publication;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Services
{
	public class CommentService : ICommentService
	{
		private readonly IRepository repo;

		public CommentService(IRepository _repo)
		{
			repo = _repo;
		}

		public async Task AddCommentAsync(CommentAddModel model, int publicationId, string userId)
		{
            var user = await repo.All<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var publication = await repo.All<Publication>()
                .Where(p => p.Id == publicationId)
                .Include(p => p.PublicationsComments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync();

            if (publication == null)
            {
                throw new ArgumentException("Invalid publication ID");
            }

            publication.PublicationsComments.Add(new PublicationComment()
            {
                PublicationId = publication.Id,
                Publication = publication,
                Comment = new Comment()
                {
                    Text = model.Text,
                    User = user,
                    UserId = userId
                }
            });

            await repo.SaveChangesAsync();
        }

		public async Task DeleteCommentAsync(int publicationId, string userId)
		{
			throw new NotImplementedException();
		}

		public async Task EditCommentAsync(CommentEditModel model)
        {
            var entity = await repo.GetByIdAsync<Comment>(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid comment");
            }

            entity.Text = model.Text;

            await repo.SaveChangesAsync();
        }

		public async Task<CommentEditModel> GetCommentForEditAsync(int commentId)
		{
            var model = await repo.GetByIdAsync<Comment>(commentId);

            if (model == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            return new CommentEditModel()
            {
                Id = model.Id,
                Text = model.Text,
                UserId = model.UserId
            };
        }
	}
}
