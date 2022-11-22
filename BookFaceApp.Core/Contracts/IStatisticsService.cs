using BookFaceApp.Core.Models;

namespace BookFaceApp.Core.Contracts
{
	public interface IStatisticsService
	{
		Task<StatisticsServiceModel> TotalLikes(int publicationId);
	}
}
