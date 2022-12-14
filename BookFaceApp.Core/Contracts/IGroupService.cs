using BookFaceApp.Core.Models.Group;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;

namespace BookFaceApp.Core.Contracts
{
    public interface IGroupService
    {
        Task AddGroupAsync(GroupAddModel model, string userId);

        Task<GroupQueryModel> GetAllGroupsAsync(
            string? category = null,
            string? searchTerm = null,
            GroupSorting sorting = GroupSorting.Newest,
            int currentPage = 1,
            int groupsPerPage = 1);

        Task<GroupViewModel> GetOneGroupAsync(int groupId);

        Task<GroupEditModel> GetGroupForEditAsync(int groupId);

        Task EditGroupAsync(GroupEditModel model);

        Task DeleteGroupAsync(int groupId);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<IEnumerable<string>> GetCategoriesNamesAsync();

        Task<bool> ExistsByIdAsync(int? groupId);

        Task<int> GetCategoryIdAsync(int? groupId);
        
        Task<bool> CategoryExistsAsync(int categoryId);

		Task<bool> IsOwnerAsync(int groupId, string userId);

        Task RequestToJoin(int groupId, string userId);

        Task AddUserToGroup(int groupId, string userId);

        Task RemoveUserFromGroup(int groupId, string userId);

        Task<bool> IsAccepted(int groupId, string userId);

        Task<IEnumerable<UserGroup>> GetAllUnacceptedUsers();

        Task<bool> IsUserInGroup(int groupId, string userId);

        Task<bool> IsUserRequested(int groupId, string userId);
    }
}
