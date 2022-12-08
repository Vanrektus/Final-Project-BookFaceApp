using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Test;
using Microsoft.Extensions.DependencyInjection;

namespace BookFaceApp.UnitTests
{
	public class PublicationServiceTests
	{
		private IServiceProvider serviceProvider;
		private InMemoryDbContext dbContext;

		[SetUp]
		public async Task Setup()
		{
			dbContext = new InMemoryDbContext();
			var serviceCollection = new ServiceCollection();

			serviceProvider = serviceCollection
				.AddSingleton(sp => dbContext.CreateContext())
				.AddSingleton<IRepository, Repository>()
				.AddSingleton<IPublicationService, PublicationService>()
				.BuildServiceProvider();

			var repo = serviceProvider.GetService<IRepository>();

			await SeedDbAsync(repo);
		}

		[Test]
		public async Task AddNewPublicationMustReturnId()
		{
			var publicationModel = new PublicationAddModel()
			{
				Title = "TestTitle",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
				CategoryId = 1,
			};

			var service = serviceProvider.GetService<IPublicationService>();

			int expectedReturnId = 1;

			var actualReturnId = await service.AddPublicationAsync(publicationModel, "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5");

			Assert.That(actualReturnId, Is.EqualTo(expectedReturnId));
		}

		[TearDown]
		public void TearDown()
		{
			dbContext.Dispose();
		}

		private async Task SeedDbAsync(IRepository repo)
		{
			var user = new User()
			{
				Id = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5",
				UserName = "Pesho",
				NormalizedUserName = "PESHO",
				FirstName = "Petar",
				LastName = "Petrov",
				Email = "pesho@mail.com",
				NormalizedEmail = "PESHO@MAIL.COM",
			};

			//var publication = new Publication()
			//{
			//	Title = "TestTitle",
			//	CategoryId = 1,
			//	UserId = user.Id,
			//};

			await repo.AddAsync(user);
			//await repo.AddAsync(publication);
			await repo.SaveChangesAsync();
		}
	}
}