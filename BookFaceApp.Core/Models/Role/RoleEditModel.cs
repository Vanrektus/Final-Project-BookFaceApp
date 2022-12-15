using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.RoleConstants;

namespace BookFaceApp.Core.Models.Role
{
    public class RoleEditModel
    {
        [Required]
        [StringLength(MaxRoleName, MinimumLength = MinRoleName)]
        public string RoleName { get; set; } = null!;

		[Required]
		public string RoleId { get; set; } = null!;

		public string[]? AddIds { get; set; }

        public string[]? DeleteIds { get; set; }
    }
}
