using BookFaceApp.Data.Entities;
using BookFaceApp.Models.Publication;

namespace BookFaceApp.Contracts
{
    public interface IPublicationService
    {
        Task<IEnumerable<PublicationViewModel>> GetAllAsync();

        //Task<PublicationViewModel> GetOneAsync(int publicationId);

        //Task<PublicationViewModel> Edit(int publicationId);

        Task AddPublicationAsync(PublicationAddModel model, string userId);

        Task<IEnumerable<PublicationViewModel>> GetMineAsync(string userId);

        Task AddCommentAsync(PublicationAddCommentModel model, int publicationId, string userId);

        Task<List<Comment>> GetCommentsOfPostAsync(int publicationId);

        Task LikePublication(int publicationId, string userId);
    }
}
