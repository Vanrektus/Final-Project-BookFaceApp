using BookFaceApp.Core.Models.Profile;
using BookFaceApp.Core.Models.Publication;

namespace BookFaceApp.Core.Contracts
{
    public interface IProfileService
    {
        Task<IEnumerable<PublicationViewModel>> GetMyProfilePublicationsAsync(string userId);

        Task<IEnumerable<PublicationViewModel>> GetUserProfilePublicationsAsync(string username);

        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
    }
}
