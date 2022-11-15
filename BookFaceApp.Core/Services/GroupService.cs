using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Group;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository repo;

        public GroupService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddGroupAsync(GroupAddModel model, string userId)
        {
            var entity = new Group()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                UserId = userId,
            };

            await repo.AddAsync<Group>(entity);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(int groupId, string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersPublications)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var group = await repo.All<Group>()
                .FirstOrDefaultAsync(b => b.Id == groupId);

            if (group == null)
            {
                throw new ArgumentException("Invalid publication ID");
            }

            if (group.UserId != user.Id)
            {
                throw new ArgumentException("Invalid owner ID");
            }

            group.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public async Task EditGroupAsync(GroupEditModel model)
        {
            var entity = await repo.GetByIdAsync<Group>(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid group");
            }

            entity.Name = model.Name;
            //entity.Category = model.Category;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync()
        {
            var entities = await repo.AllReadonly<Group>()
                .Where(g => g.IsDeleted == false)
                .Include(g => g.User)
                .Include(g => g.UsersGroups)
                .Include(g => g.PublicationsGroups)
                .Include(g => g.Category)
                .ToListAsync();

            return entities
                .Select(g => new GroupViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    UserId = g.UserId,
                    Category = g.Category.Name,
                    UsersGroups = g.UsersGroups,
                    PublicationsGroups = g.PublicationsGroups,
                });
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await repo.All<Category>()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<GroupEditModel> GetGroupForEditAsync(int groupId)
        {
            var model = await repo.GetByIdAsync<Group>(groupId);

            if (model == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            return new GroupEditModel()
            {
                Id = model.Id,
                Name = model.Name,
                UserId = model.UserId
            };
        }

        public async Task<GroupViewModel> GetOneGroupAsync(int groupId)
        {
            var model = await repo.AllReadonly<Group>()
                .Where(g => g.Id == groupId)
                .Include(g => g.User)
                .Include(g => g.UsersGroups)
                .Include(g => g.PublicationsGroups)
                .Include(g => g.Category)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null!;
                //throw new ArgumentException("Invalid publication ID");
            }

            var user = await repo.GetByIdAsync<User>(model.UserId);

            return new GroupViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                UserId = model.UserId,
                Category = model.Category.Name,
                UsersGroups = model.UsersGroups,
                PublicationsGroups = model.PublicationsGroups,
            };
        }
    }
}
