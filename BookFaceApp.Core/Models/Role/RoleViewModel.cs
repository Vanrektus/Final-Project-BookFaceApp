using Microsoft.AspNetCore.Identity;

namespace BookFaceApp.Core.Models.Role
{
    public class RoleViewModel
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<Infrastructure.Data.Entities.User> Users { get; set; }

        public IEnumerable<Infrastructure.Data.Entities.User> NonUsers { get; set; }
    }
}
