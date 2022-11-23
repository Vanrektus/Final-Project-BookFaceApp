using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data;
using BookFaceApp.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class BookFaceAppApiServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IStatisticsService, StatisticsService>();

			return services;
		}

		public static IServiceCollection AddBookFaceAppDbContext(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config.GetConnectionString("DefaultConnection");

			services.AddDbContext<BookFaceAppDbContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddScoped<IRepository, Repository>();

			return services;
		}
	}
}
