using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.Controllers.Api
{
	[ApiController]
	[Route("/api/statistics")]
	public class StatisticsApiController : Controller
	{
		private readonly IStatisticsService statisticsService;

		public StatisticsApiController(IStatisticsService _statisticsService)
		{
			statisticsService= _statisticsService;
		}

		[HttpGet]
		public Task<StatisticsServiceModel> GetLikes(int id)
		{
			return statisticsService.TotalLikes(id);
		}
	}
}
