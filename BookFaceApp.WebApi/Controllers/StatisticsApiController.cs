using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookFaceApp.WebApi.Controllers
{
	[Route("api/statistics")]
	[ApiController]
	public class StatisticsApiController : ControllerBase
	{
		private readonly IStatisticsService statisticsService;

		public StatisticsApiController(IStatisticsService _statisticsService)
		{
			statisticsService = _statisticsService;
		}

		/// <summary>
		/// Gets the total likes of a publication
		/// </summary>
		/// <param name="id">Publication id</param>
		/// <returns>Total likes</returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetLikes(int id)
		{
			var model = await statisticsService.TotalLikes(id);

			return Ok(model);
		}
	}
}
