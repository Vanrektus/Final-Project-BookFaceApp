using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Models.Group
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Category { get; set; } = null!;

        public List<UserGroup> UsersGroups { get; set; } = null!;

        public List<PublicationGroup> PublicationsGroups { get; set; } = null!;
    }
}
