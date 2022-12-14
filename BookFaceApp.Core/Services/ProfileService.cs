using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Profile;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository repo;

        public ProfileService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            var entities = await repo.All<User>()
                .Include(u => u.Publications.Where(p => p.IsDeleted == false && p.GroupId == null))
                .Include(u => u.ProfilePicture)
                .ToListAsync();

            return entities
                .Select(u => new AllUsersViewModel()
                {
                    UserName = u.UserName,
                    ProfilePicture = u.ProfilePicture!.ImageToString,
                    Publications = u.Publications,
                });
        }

        public async Task<IEnumerable<PublicationViewModel>> GetMyProfilePublicationsAsync(string userId)
        {
            var entities = await repo.AllReadonly<Publication>()
                .Where(p => p.UserId == userId)
                .Where(p => p.IsDeleted == false)
                .Where(p => p.GroupId == null)
                .Include(p => p.User)
                .ThenInclude(u => u.ProfilePicture)
                .Include(p => p.PublicationsComments
                .Where(pc => pc.Comment.IsDeleted == false))
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .Include(p => p.UsersPublications)
                .ThenInclude(up => up.User)
                .Include(p => p.Category)
                .ToListAsync();

            return entities
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl!,
                    UserId = p.UserId,
                    User = p.User,
                    Category = p.Category.Name,
                    PublicationsComments = p.PublicationsComments,
                    UsersPublications = p.UsersPublications,
                });
        }

        public async Task<IEnumerable<PublicationViewModel>> GetUserProfilePublicationsAsync(string username)
        {
            var entities = await repo.AllReadonly<Publication>()
                .Where(p => p.User.UserName == username)
                .Where(p => p.IsDeleted == false)
                .Where(p => p.GroupId == null)
                .Include(p => p.User)
                .ThenInclude(u => u.ProfilePicture)
                .Include(p => p.PublicationsComments
                .Where(pc => pc.Comment.IsDeleted == false))
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .Include(p => p.UsersPublications)
                .ThenInclude(up => up.User)
                .Include(p => p.Category)
                .ToListAsync();

            return entities
                .Select(p => new PublicationViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl!,
                    UserId = p.UserId,
                    User = p.User,
                    Category = p.Category.Name,
                    PublicationsComments = p.PublicationsComments,
                    UsersPublications = p.UsersPublications,
                });
        }

        public async Task<string> GetProfilePictureAsync(string userId)
        {
            var user = await repo.AllReadonly<User>()
                .Include(u => u.ProfilePicture)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user!.ProfilePicture!.ImageToString;
        }
    }
}
