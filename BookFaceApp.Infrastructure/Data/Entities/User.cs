using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.UserConstants;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxUserFirstName)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(MaxUserLastName)]
        public string LastName { get; set; } = null!;

        public List<Publication> Publications { get; set; } = new List<Publication>();

        public List<UserPublication> UsersPublications { get; set; } = new List<UserPublication>();

        //public List<User> Friends { get; set; }
    }
}
