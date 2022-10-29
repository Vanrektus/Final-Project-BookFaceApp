using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookFaceApp.Data.Entities
{
    public class UserPublication
    {
        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public int PublicationId { get; set; }

        [ForeignKey(nameof(PublicationId))]
        public Publication Publication { get; set; } = null!;

        public bool isLiked { get; set; }
    }
}
