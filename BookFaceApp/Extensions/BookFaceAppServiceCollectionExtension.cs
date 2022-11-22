using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BookFaceAppServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPublicationService, PublicationService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }
    }
}
