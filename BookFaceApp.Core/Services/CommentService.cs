using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Comment;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
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

            var publication = await repo.All<Publication>()
                .Where(p => p.Id == publicationId)
                .Include(p => p.PublicationsComments
                .Where(pc => pc.Comment.IsDeleted == false))
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync();

            publication!.PublicationsComments.Add(new PublicationComment()
            {
                PublicationId = publication.Id,
                Publication = publication,
                Comment = new Comment()
                {
                    Text = model.Text,
                    User = user!,
                    UserId = userId
                }
            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await repo.All<Comment>()
                .FirstOrDefaultAsync(b => b.Id == commentId);

            comment!.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public async Task EditCommentAsync(CommentEditModel model)
        {
            var entity = await repo.GetByIdAsync<Comment>(model.Id);

            entity!.Text = model.Text;

            await repo.SaveChangesAsync();
        }

        public async Task<CommentEditModel> GetCommentForEditAsync(int commentId)
        {
            var model = await repo.All<Comment>()
                .Where(c => c.Id == commentId)
                .Include(c => c.PublicationsComments)
                .FirstOrDefaultAsync();

            var publicationId = model!.PublicationsComments
                .Select(pc => pc.PublicationId)
                .FirstOrDefault();

            return new CommentEditModel()
            {
                Id = model.Id,
                Text = model.Text,
                Publicationid = publicationId,
                UserId = model.UserId
            };
        }

        public async Task<bool> ExistsAsync(int commentId)
            => await repo.AllReadonly<Comment>()
                .AnyAsync(g => g.Id == commentId);

        public async Task<bool> IsOwner(int commentId, string userId)
            => (await repo.GetByIdAsync<Comment>(commentId)).UserId == userId;

        public async Task<int> GetPublicationIdByCommentIdAsync(int commentId)
        {
            var comment = await repo.AllReadonly<Comment>()
                .Include(c => c.PublicationsComments)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            int publicationId = comment!.PublicationsComments
                .FirstOrDefault(pc => pc.CommentId == commentId)!.PublicationId;

            return publicationId;
        }
            
    }
}
