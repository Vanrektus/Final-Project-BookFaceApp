using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookFaceApp.Data.Entities
{
    public class PublicationComment
    {
        [Key]
        public int PublicationId { get; set; }

        [ForeignKey(nameof(PublicationId))]
        public Publication Publication { get; set; }

        [Key]
        public int CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}
