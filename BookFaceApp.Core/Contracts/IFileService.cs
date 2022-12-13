using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Contracts
{
	public interface IFileService
	{
		Task ChangePictureAsync(ProfilePicture file, string userId);
	}
}
