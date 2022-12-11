using BookFaceApp.Core.Contracts;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;

namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationViewModel : IPublicationModel
    {
        public int Id { get; init; }

        public string Title { get; init; } = null!;

        public string ImageUrl { get; init; } = null!;

        public string UserId { get; init; } = null!;

        public Infrastructure.Data.Entities.User User { get; set; } = null!;

        public int? GroupId { get; set; }

        public string Category { get; init; } = null!;

        public List<PublicationComment> PublicationsComments { get; init; } = null!;

        public List<UserPublication> UsersPublications { get; init; } = null!;
    }
}
