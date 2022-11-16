using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookFaceApp.Infrastructure.Data.Entities.Relationships
{
    public class UserUser
    {
        [Required]
        public string UserOneId { get; set; } = null!;

        [ForeignKey(nameof(UserOneId))]
        public User UserOne { get; set; } = null!;

        [Required]
        public string UserTwoId { get; set; } = null!;

        [ForeignKey(nameof(UserTwoId))]
        public User UserTwo { get; set; } = null!;
    }
}
