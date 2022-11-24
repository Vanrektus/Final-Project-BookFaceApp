using BookFaceApp.Infrastructure.Data.Entities.Relationships;

namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationViewModel
    {
        public int Id { get; init; }

        public string Title { get; init; } = null!;

        public string ImageUrl { get; init; } = null!;

        public string UserName { get; init; } = null!;

        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;

        public string UserId { get; init; } = null!;

        public string Category { get; init; } = null!;

        public List<PublicationComment> PublicationsComments { get; init; } = null!;

        public List<UserPublication> UsersPublications { get; init; } = null!;
    }
}
