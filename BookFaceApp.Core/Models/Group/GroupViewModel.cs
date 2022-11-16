using BookFaceApp.Infrastructure.Data.Entities.Relationships;

namespace BookFaceApp.Core.Models.Group
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public BookFaceApp.Infrastructure.Data.Entities.User Owner { get; set; } = null!;

        public string Category { get; set; } = null!;

        public List<BookFaceApp.Infrastructure.Data.Entities.Publication> Publications { get; set; } = null!;

        public List<UserGroup> UsersGroups { get; set; } = null!;
    }
}
