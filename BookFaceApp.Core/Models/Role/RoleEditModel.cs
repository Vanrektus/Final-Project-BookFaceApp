using System.ComponentModel.DataAnnotations;

namespace BookFaceApp.Core.Models.Role
{
    public class RoleEditModel
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string[]? AddIds { get; set; }

        public string[]? DeleteIds { get; set; }
    }
}
