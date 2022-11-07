using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Contracts
{
    public interface IPublicationService
    {
        Task AddPublicationAsync(PublicationAddModel model, string userId);

        Task<IEnumerable<PublicationViewModel>> GetAllPublicationsAsync();

        Task<PublicationViewModel> GetOnePublicationAsync(int publicationId);

        Task<PublicationEditModel> GetPublicationForEditAsync(int publicationId);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task EditPublicationAsync(PublicationEditModel model);

        Task<IEnumerable<PublicationViewModel>> GetUserPublicationsAsync(string userId);

        Task LikePublicationAsync(int publicationId, string userId);

        Task DeletePublicationAsync(int publicationId, string userId);
    }
}
