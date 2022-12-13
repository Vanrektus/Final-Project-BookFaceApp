using System.ComponentModel.DataAnnotations;

namespace BookFaceApp.Core.Models.Role
{
    public class RoleEditModel
    {
        [Required]
        public string RoleName { get; set; } = null!;

		[Required]
		public string RoleId { get; set; } = null!;

		public string[]? AddIds { get; set; }

        public string[]? DeleteIds { get; set; }
    }
}
