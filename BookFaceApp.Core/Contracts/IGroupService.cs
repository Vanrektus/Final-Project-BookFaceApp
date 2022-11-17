using BookFaceApp.Core.Models.Group;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Contracts
{
    public interface IGroupService
    {
        Task AddGroupAsync(GroupAddModel model, string userId);

        Task<IEnumerable<GroupViewModel>> GetAllGroupsAsyncOLD();

        Task<GroupQueryModel> GetAllGroupsAsync(
            string? category = null,
            string? searchTerm = null,
            GroupSorting sorting = GroupSorting.Newest,
            int currentPage = 1,
            int groupsPerPage = 1);

        Task<GroupViewModel> GetOneGroupAsync(int groupId);

        Task<GroupEditModel> GetGroupForEditAsync(int groupId);

        Task EditGroupAsync(GroupEditModel model);

        Task DeleteGroupAsync(int groupId, string userId);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<IEnumerable<string>> GetCategoriesNamesAsync();

        Task AddGroupPublicationAsync(PublicationAddModel model, string userId);
    }
}
