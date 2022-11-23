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
					Group = entity
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
					UsersGroups = p.UsersGroups
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
				UserId = model.UserId
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
				.ThenInclude(p => p.PublicationsComments)
				.Include(g => g.Publications.Where(p => p.IsDeleted == false))
				.ThenInclude(p => p.User)
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

		public async Task<bool> IsOwner(int groupId, string userId) 
			=> (await repo.GetByIdAsync<Group>(groupId)).UserId == userId;
	}
}
