using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Group;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
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
			var user = await repo.GetByIdAsync<User>(userId);

			var entity = new Group()
			{
				Name = model.Name,
				CategoryId = model.CategoryId,
				UserId = userId,
				User = user
			};

			await repo.AddAsync<Group>(entity);

			if (!user.UsersGroups.Any(ug => ug.GroupId == entity.Id))
			{
				user.UsersGroups.Add(new UserGroup()
				{
					UserId = user.Id,
					User = user,
					GroupId = entity.Id,
					Group = entity,
					IsAccepted = true
				});
			}

			await repo.SaveChangesAsync();
		}

		public async Task DeleteGroupAsync(int groupId)
		{
			var group = await repo.All<Group>()
				.FirstOrDefaultAsync(b => b.Id == groupId);

			group!.IsDeleted = true;

			await repo.SaveChangesAsync();
		}

		public async Task EditGroupAsync(GroupEditModel model)
		{
			var entity = await repo.GetByIdAsync<Group>(model.Id);

			entity.Name = model.Name;
			entity.CategoryId = model.CategoryId;

			await repo.SaveChangesAsync();
		}

		public async Task<GroupQueryModel> GetAllGroupsAsync(
			string? category = null,
			string? searchTerm = null,
			GroupSorting sorting = GroupSorting.Newest,
			int currentPage = 1,
			int groupsPerPage = 1)
		{
			var result = new GroupQueryModel();

			var groups = repo.AllReadonly<Group>()
				.Include(g => g.UsersGroups.Where(ug => ug.IsAccepted == true))
				.Include(g => g.Publications.Where(p => p.IsDeleted == false))
				.Where(g => g.IsDeleted == false);

			if (!string.IsNullOrEmpty(category))
			{
				groups = groups
					.Where(p => p.Category.Name == category);
			}

			if (!string.IsNullOrEmpty(searchTerm))
			{
				searchTerm = $"%{searchTerm.ToUpper()}%";

				groups = groups
					.Where(p => EF.Functions.Like(p.Name.ToUpper(), searchTerm));
			}

			groups = sorting switch
			{
				GroupSorting.MostUsers => groups
				.OrderByDescending(g => g.UsersGroups.Count),
				GroupSorting.MostPublications => groups
				.OrderByDescending(g => g.Publications.Count),
				_ => groups.OrderByDescending(p => p.Id)
			};

			result.Groups = await groups
				.Skip((currentPage - 1) * groupsPerPage)
				.Take(groupsPerPage)
				.Select(p => new GroupViewModel()
				{
					Id = p.Id,
					Name = p.Name,
					Category = p.Category.Name,
					UserId = p.UserId,
					Owner = p.User,
					Publications = p.Publications.Where(p => p.IsDeleted == false).ToList(),
					UsersGroups = p.UsersGroups.Where(ug => ug.IsAccepted == true).ToList()
				})
				.ToListAsync();

			result.TotalGroupsCount = await groups.CountAsync();

			return result;
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync()
			=> await repo.All<Category>()
			.OrderBy(c => c.Name)
			.ToListAsync();

		public async Task<GroupEditModel> GetGroupForEditAsync(int groupId)
		{
			var model = await repo.GetByIdAsync<Group>(groupId);

			return new GroupEditModel()
			{
				Id = model.Id,
				Name = model.Name,
				UserId = model.UserId,
				CategoryId = model.CategoryId,
				Categories = await GetCategoriesAsync()
			};
		}

		public async Task<GroupViewModel> GetOneGroupAsync(int groupId)
		{
			var model = await repo.AllReadonly<Group>()
				.Where(g => g.Id == groupId)
				.Include(g => g.User)
				.Include(g => g.Publications.Where(p => p.IsDeleted == false))
				.ThenInclude(p => p.UsersPublications)
				.Include(g => g.Publications.Where(p => p.IsDeleted == false))
				.ThenInclude(p => p.PublicationsComments.Where(pc => pc.Comment.IsDeleted == false))
				.Include(g => g.Publications.Where(p => p.IsDeleted == false))
				.ThenInclude(p => p.User)
				.Include(g => g.Publications.Where(p => p.IsDeleted == false))
				.ThenInclude(p => p.Category)
				.Include(g => g.Category)
				.FirstOrDefaultAsync();

			var user = await repo.GetByIdAsync<User>(model!.UserId);

			return new GroupViewModel()
			{
				Id = model.Id,
				Name = model.Name,
				UserId = model.UserId,
				Category = model.Category.Name,
				Publications = model.Publications,
			};
		}

		public async Task<int> GetCategoryIdAsync(int? groupId)
		{
			if (groupId == null)
			{
				return 0;
			}

			var group = await repo.GetByIdAsync<Group>(groupId);

			return group.CategoryId;
		}

		public async Task<IEnumerable<string>> GetCategoriesNamesAsync()
			=> await repo.AllReadonly<Category>()
			.Select(c => c.Name)
			.Distinct()
			.ToListAsync();

		public async Task<bool> ExistsByIdAsync(int? groupId)
			=> await repo.AllReadonly<Group>().AnyAsync(g => g.Id == groupId);

		public async Task<bool> CategoryExistsAsync(int categoryId) 
			=> await repo.GetByIdAsync<Category>(categoryId) != null;

		public async Task<bool> IsOwnerAsync(int groupId, string userId) 
			=> (await repo.GetByIdAsync<Group>(groupId)).UserId == userId;

        public async Task RequestToJoin(int groupId, string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGroups)
                .FirstOrDefaultAsync();

            var group = await repo.All<Group>()
                .FirstOrDefaultAsync(b => b.Id == groupId);

            if (!(user!.UsersGroups.Any(ug => ug.GroupId == groupId)))
            {
                user.UsersGroups.Add(new UserGroup()
                {
                    UserId = user.Id,
                    User = user,
                    GroupId = group!.Id,
                    Group = group,
					IsAccepted = false
                });
            }

            await repo.SaveChangesAsync();
        }

        public async Task AddUserToGroup(int groupId, string userId)
		{
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGroups)
                .FirstOrDefaultAsync();

            var group = await repo.All<Group>()
                .FirstOrDefaultAsync(b => b.Id == groupId);

			var userGroup = user!.UsersGroups.First(ug => ug.GroupId == groupId && ug.UserId == userId);

            if (userGroup != null)
            {
				userGroup.IsAccepted = true;
            }

            await repo.SaveChangesAsync();
        }

		public async Task RemoveUserFromGroup(int groupId, string userId)
		{
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGroups)
                .FirstOrDefaultAsync();

            var group = await repo.All<Group>()
                .FirstOrDefaultAsync(b => b.Id == groupId);

			if (user!.UsersGroups.Any(ug => ug.GroupId == groupId))
			{
                user.UsersGroups.Remove(user.UsersGroups
                    .FirstOrDefault(up => up.GroupId == groupId)!);
            }

            await repo.SaveChangesAsync();
        }

		public async Task<bool> IsAccepted(int groupId, string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGroups)
                .FirstOrDefaultAsync();

            var group = await repo.All<Group>()
                .FirstOrDefaultAsync(b => b.Id == groupId);

            return user!.UsersGroups.First(ug => ug.GroupId == groupId && ug.UserId == userId).IsAccepted;
        }

        public async Task<IEnumerable<UserGroup>> GetAllUnacceptedUsers()
		{
			return await repo.AllReadonly<UserGroup>()
				.Include(ug => ug.User)
				.Include(ug => ug.Group)
				.Where(ug => ug.IsAccepted == false)
				.ToListAsync();
		}

		public async Task<bool> IsUserInGroup(int groupId, string userId)
		{
			var userGroup = await repo.All<UserGroup>().
			FirstOrDefaultAsync(ug => ug.GroupId == groupId && ug.UserId == userId);

			if (userGroup != null && userGroup.IsAccepted == true)
			{
				return true;
			}

			return false;
        }

		public async Task<bool> IsUserRequested(int groupId, string userId)
		{
			var userGroup = await repo.All<UserGroup>().
            FirstOrDefaultAsync(ug => ug.GroupId == groupId && ug.UserId == userId);

			if (userGroup != null)
			{
				return userGroup.IsAccepted == false ? true : false;
			}

			return false;
        }
    }
}
