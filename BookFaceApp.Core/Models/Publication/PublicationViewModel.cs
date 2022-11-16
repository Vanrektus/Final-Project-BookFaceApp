using BookFaceApp.Infrastructure.Data.Entities.Relationships;

namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Category { get; set; } = null!;

        public List<PublicationComment> PublicationsComments { get; set; } = null!;

        public List<UserPublication> UsersPublications { get; set; } = null!;
    }
}
