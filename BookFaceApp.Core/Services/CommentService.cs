using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Comment;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Core.Services
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
                .Include(p => p.PublicationsComments
                .Where(pc => pc.Comment.IsDeleted == false))
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

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersPublications)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var comment = await repo.All<Comment>()
                .FirstOrDefaultAsync(b => b.Id == commentId);

            if (comment == null)
            {
                throw new ArgumentException("Invalid comment ID");
            }

            if (comment.UserId != user.Id)
            {
                throw new ArgumentException("Invalid owner ID");
            }

            comment.IsDeleted = true;

            await repo.SaveChangesAsync();
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
            var model = await repo.All<Comment>()
                .Where(c => c.Id == commentId)
                .Include(c => c.PublicationsComments)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            var publicationId = model.PublicationsComments
                .Select(pc => pc.PublicationId)
                .FirstOrDefault();

            if (publicationId == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            return new CommentEditModel()
            {
                Id = model.Id,
                Text = model.Text,
                Publicationid = publicationId,
                UserId = model.UserId
            };
        }
    }
}
