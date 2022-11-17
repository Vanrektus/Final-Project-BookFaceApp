using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Core.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly IRepository repo;

        public PublicationService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddPublicationAsync(PublicationAddModel model, string userId)
        {
            var entity = new Publication()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                UserId = userId,
            };

            if (model.GroupId != null)
            {
                entity.GroupId = model.GroupId;
            }

            await repo.AddAsync<Publication>(entity);
            await repo.SaveChangesAsync();
        }

        public async Task EditPublicationAsync(PublicationEditModel model)
        {
            var entity = await repo.GetByIdAsync<Publication>(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid publication");
            }

            entity.Title = model.Title;
            entity.ImageUrl = model.ImageUrl;
            entity.CategoryId = model.CategoryId;

            await repo.SaveChangesAsync();
        }

        public async Task<PublicationQueryModel> GetAllPublicationsAsync(
            string? category = null, 
            string? searchTerm = null, 
            PublicationSorting sorting = PublicationSorting.Newest, 
            int currentPage = 1, 
            int publicationsPerPage = 1)
        {
            var result = new PublicationQueryModel();

            var publications = repo.AllReadonly<Publication>()
                .Where(p => p.GroupId == null);

            if (!string.IsNullOrEmpty(category))
            {
                publications = publications
                    .Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = $"%{searchTerm.ToUpper()}%";

                publications = publications
                    .Where(p => EF.Functions.Like(p.Title.ToUpper(), searchTerm));
            }

            publications = sorting switch
            {
                PublicationSorting.MostLiked => publications
                .OrderByDescending(p => p.UsersPublications.Count),
                PublicationSorting.MostCommented => publications
                .OrderByDescending(p => p.PublicationsComments.Count),
                _ => publications.OrderByDescending(p => p.Id)
            };

            result.Publications = await publications
                .Skip((currentPage - 1) * publicationsPerPage)
                .Take(publicationsPerPage)
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl!,
                    Category = p.Category.Name,
                    UserId = p.UserId,
                    UserName = p.User.UserName,
                    UsersPublications = p.UsersPublications,
                    PublicationsComments = p.PublicationsComments
                })
                .ToListAsync();

            result.TotalPublicationsCount = await publications.CountAsync();

            return result;
        }

        public async Task<PublicationViewModel> GetOnePublicationAsync(int publicationId)
        {
            var model = await repo.AllReadonly<Publication>()
                .Where(p => p.Id == publicationId)
                .Include(p => p.User)
                .Include(p => p.PublicationsComments
                .Where(pc => pc.Comment.IsDeleted == false))
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .Include(p => p.UsersPublications)
                .ThenInclude(up => up.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            var user = await repo.GetByIdAsync<User>(model.UserId);

            return new PublicationViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ImageUrl = model.ImageUrl!,
                UserName = user.UserName,
                UserId = model.UserId,
                Category = model.Category.Name,
                PublicationsComments = model.PublicationsComments,
                UsersPublications = model.UsersPublications
            };
        }

        public async Task<PublicationEditModel> GetPublicationForEditAsync(int publicationId)
        {
            var model = await repo.GetByIdAsync<Publication>(publicationId);

            if (model == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            return new PublicationEditModel()
            {
                Id = model.Id,
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                UserId = model.UserId,
                CategoryId = model.CategoryId,
                Categories = await GetCategoriesAsync(),
            };
        }

        public async Task LikePublicationAsync(int publicationId, string userId)
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
                throw new ArgumentException("Invalid publication ID");
            }

            if (!user.UsersPublications.Any(up => up.PublicationId == publicationId))
            {
                user.UsersPublications.Add(new UserPublication()
                {
                    UserId = user.Id,
                    User = user,
                    PublicationId = publication.Id,
                    Publication = publication,
                });
            }
            else
            {
                user.UsersPublications.Remove(user.UsersPublications
                    .FirstOrDefault(up => up.PublicationId == publicationId)!);
            }

            await repo.SaveChangesAsync();
        }

        public async Task DeletePublicationAsync(int publicationId, string userId)
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
                throw new ArgumentException("Invalid publication ID");
            }

            if (publication.UserId != user.Id)
            {
                throw new ArgumentException("Invalid owner ID");
            }

            publication.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() 
        { 
            return await repo.All<Category>()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<PublicationViewModel>> GetTop3PublicationsAsync()
        {
            var entities = await repo.AllReadonly<Publication>()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.GroupId == null)
                .OrderByDescending(p => p.UsersPublications.Count)
                .ThenByDescending(p => p.PublicationsComments.Count)
                .Include(p => p.User)
                .Include(p => p.PublicationsComments
                .Where(pc => pc.Comment.IsDeleted == false))
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .Include(p => p.UsersPublications)
                .ThenInclude(up => up.User)
                .Include(p => p.Category)
                .Take(3)
                .ToListAsync();

            return entities
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl!,
                    UserName = p.User.UserName,
                    UserId = p.UserId,
                    Category = p.Category.Name,
                    PublicationsComments = p.PublicationsComments,
                    UsersPublications = p.UsersPublications,
                });
        }

        public async Task<IEnumerable<string>> GetCategoriesNamesAsync()
        {
            return await repo.AllReadonly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }
    }
}
