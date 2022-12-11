using BookFaceApp.Core.Contracts;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;

namespace BookFaceApp.Core.Services
{
	public class FileService : IFileService
	{
		private readonly IRepository repo;

		public FileService(IRepository _repo)
		{
			repo = _repo;
		}

		public async Task SavePictureAsync(ProfilePicture file)
		{
			await repo.AddAsync(file);
			await repo.SaveChangesAsync();
		}
	}
}
