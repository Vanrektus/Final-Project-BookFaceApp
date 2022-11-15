using BookFaceApp.Core.Models.Group;
using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Contracts
{
    public interface IGroupService
    {
        Task AddGroupAsync(GroupAddModel model, string userId);

        Task<IEnumerable<GroupViewModel>> GetAllGroupsAsync();

        Task<GroupViewModel> GetOneGroupAsync(int groupId);

        Task<GroupEditModel> GetGroupForEditAsync(int groupId);

        Task EditGroupAsync(GroupEditModel model);

        Task DeleteGroupAsync(int groupId, string userId);

        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
