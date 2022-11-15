using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookFaceApp.Infrastructure.Data.DataConstants.GroupConstants;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxGroupName)]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public List<UserGroup> UsersGroups { get; set; } = new List<UserGroup>();

        public List<PublicationGroup> PublicationsGroups { get; set; } = new List<PublicationGroup>();
    }
}
