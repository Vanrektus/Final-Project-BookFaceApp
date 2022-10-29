using Microsoft.AspNetCore.Identity;

namespace BookFaceApp.Data.Entities
{
    public class User : IdentityUser
    {
        public List<Publication> Publications { get; set; } = new List<Publication>();

        public List<UserPublication> UsersPublications { get; set; } = new List<UserPublication>();
    }
}
