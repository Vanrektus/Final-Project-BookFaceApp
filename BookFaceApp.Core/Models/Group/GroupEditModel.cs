using BookFaceApp.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.GroupConstants;

namespace BookFaceApp.Core.Models.Group
{
    public class GroupEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxGroupName)]
        public string Name { get; set; } = null!;

		[Required]
		public int CategoryId { get; set; }

		public IEnumerable<Category> Categories { get; set; } = new List<Category>();

		public string? UserId { get; set; }
    }
}
