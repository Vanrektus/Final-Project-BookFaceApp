using BookFaceApp.Contracts;
using BookFaceApp.Data.Common;
using BookFaceApp.Data.Entities;
using BookFaceApp.Models.Publication;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookFaceApp.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IRepository repo;

        public PublicationService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddCommentAsync(PublicationAddCommentModel model, int publicationId, string userId)
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

        public async Task AddPublicationAsync(PublicationAddModel model, string userId)
        {
            var entity = new Publication()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                UserId = userId,
            };

            await repo.AddAsync<Publication>(entity);
            await repo.SaveChangesAsync();
        }

        //public async Task<PublicationViewModel> Edit(int publicationId)
        //{
        //    var model = await repo.All<Publication>()
        //        .Where(p => p.Id == publicationId)
        //        .Select(p => new PublicationViewModel()
        //        {
        //            Id = p.Id,
        //            Title = p.Title,
        //            ImageUrl = p.ImageUrl
        //        })
        //        .FirstOrDefaultAsync();

        //    return model;
        //}

        public async Task<IEnumerable<PublicationViewModel>> GetAllAsync()
        {
            var entities = await repo.All<Publication>()
                .Include(p => p.PublicationsComments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .Include(p => p.UsersPublications)
                .ThenInclude(up => up.User)
                .ToListAsync();

            return entities
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    UserName = p.User.UserName,
                    UserId = p.UserId,
                    PublicationsComments = p.PublicationsComments,
                    UsersPublications = p.UsersPublications,
                });
        }

        public async Task<List<Comment>> GetCommentsOfPostAsync(int publicationId)
        {
            var publication = await repo.All<Publication>()
                .FirstOrDefaultAsync(p => p.Id == publicationId);

            if (publication == null)
            {
                throw new ArgumentException("Invalid publication ID");
            }



            return await repo.All<Comment>().ToListAsync();
        }

        public async Task<IEnumerable<PublicationViewModel>> GetMineAsync(string userId)
        {
            var entities = await repo.All<Publication>()
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return entities
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl
                });
        }

        public async Task LikePublication(int publicationId, string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersPublications)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var publication = await repo.All<Publication>()
                .FirstOrDefaultAsync(b => b.Id == publicationId);

            if (publication == null)
            {
                throw new ArgumentException("Invalid book ID");
            }

            if (!user.UsersPublications.Any(up => up.PublicationId == publicationId))
            {
                user.UsersPublications.Add(new UserPublication()
                {
                    UserId = user.Id,
                    User = user,
                    PublicationId = publication.Id,
                    Publication = publication,
                    isLiked = true
                });
            }
            else
            {
                user.UsersPublications.Remove(user.UsersPublications
                    .FirstOrDefault(up => up.PublicationId == publicationId)!);
            }

            await repo.SaveChangesAsync();
        }

        //public async Task<PublicationViewModel> GetOneAsync(int publicationId)
        //{
        //    return await repo.All<Publication>()
        //        .Where(p => p.Id == publicationId)
        //        .Select(p => new PublicationViewModel()
        //        {
        //            Id = p.Id,
        //            Title = p.Title,
        //            ImageUrl = p.ImageUrl
        //        })
        //        .FirstOrDefaultAsync();
        //}
    }
}
