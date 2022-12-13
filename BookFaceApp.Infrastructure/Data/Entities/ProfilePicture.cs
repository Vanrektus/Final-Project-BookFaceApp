using System.ComponentModel.DataAnnotations;

namespace BookFaceApp.Infrastructure.Data.Entities
{
	public class ProfilePicture
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public string FileName { get; set; } = null!;

		[Required]
		public byte[] Content { get; set; } = null!;

		[Required]
		public string ImageToString { get; set; } = null!;

		[Required]
		public string UserId { get; set; } = null!;
	}
}
