using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Contracts
{
	public interface IFileService
	{
		Task SavePictureAsync(ProfilePicture file);
	}
}
