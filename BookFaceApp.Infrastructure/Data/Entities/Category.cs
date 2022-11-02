using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.CategoryConstants;

namespace BookFaceApp.Infrastructure.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCategoryName)]
        public string Name { get; set; } = null!;

        public List<Publication> Publications { get; set; } = new List<Publication>();
    }
}
