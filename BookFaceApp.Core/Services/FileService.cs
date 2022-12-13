using BookFaceApp.Core.Contracts;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Core.Services
{
	public class FileService : IFileService
	{
		private readonly IRepository repo;

		public FileService(IRepository _repo)
		{
			repo = _repo;
		}
		
		public async Task ChangePictureAsync(ProfilePicture pictureModel, string userId)
		{
			var pictureToChange = await repo.All<ProfilePicture>()
				.FirstOrDefaultAsync(pp => pp.UserId == userId);

			pictureToChange!.FileName = pictureModel.FileName;
			pictureToChange.Content = pictureModel.Content;
			pictureToChange.ImageToString = pictureModel.ImageToString;

			await repo.SaveChangesAsync();
		}
	}
}
