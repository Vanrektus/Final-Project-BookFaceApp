using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class PublicationGroup
    {
        [Key]
        public int PublicationId { get; set; }

        [ForeignKey(nameof(PublicationId))]
        public Publication Publication { get; set; } = null!;

        [Key]
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; } = null!;
    }
}
