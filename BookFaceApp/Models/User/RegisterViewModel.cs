using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.UserConstants;

namespace BookFaceApp.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(MaxUserUserName, MinimumLength = MinUserUserName)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(MaxUserEmail, MinimumLength = MinUserEmail)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(MaxUserPassword, MinimumLength = MinUserPassword)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
