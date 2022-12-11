using System.ComponentModel.DataAnnotations;

namespace BookFaceApp.Infrastructure.Data.Entities
{
	public class ProfilePicture
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		public string FileName { get; set; } = null!;

		public byte[] Content { get; set; } = null!;

		public string UserId { get; set; } = null!;

		//[ForeignKey(nameof(UserId))]
		//public User User { get; set; } = null!;
	}
}
