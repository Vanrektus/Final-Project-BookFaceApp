namespace BookFaceApp.Core.Models.Profile
{
    public class AllUsersViewModel
    {
        public string UserName { get; set; } = null!;

        public List<Infrastructure.Data.Entities.Publication> Publications { get; set; } = new List<Infrastructure.Data.Entities.Publication>();
    }
}
