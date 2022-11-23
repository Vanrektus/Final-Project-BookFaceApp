using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Core.Services
{
	public class StatisticsService : IStatisticsService
	{
		private readonly IRepository repo;

		public StatisticsService(IRepository _repo)
		{
			repo= _repo;
		}

		public async Task<StatisticsServiceModel> TotalLikes(int publicationId)
		{
			var publication = await repo.AllReadonly<Publication>()
				.Where(p => p.Id == publicationId)
				.Include(p => p.UsersPublications)
				.FirstOrDefaultAsync();

            return new StatisticsServiceModel
			{
				Id = publication.Id,
				TotalLikes = publication.UsersPublications.Count()
			};
		}
	}
}
