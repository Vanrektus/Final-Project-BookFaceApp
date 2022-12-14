using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.Extensions.DependencyInjection;

namespace BookFaceApp.Test.ServicesTests
{
    public class ProfileServiceTests
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
                .AddSingleton<IProfileService, ProfileService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo!);
        }



        // --- Get All Users TESTS ---
        [Test]
        public async Task GetAllUsersShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IProfileService>();

            Assert.DoesNotThrowAsync(async () => await service!.GetAllUsersAsync());

            var users = await service!.GetAllUsersAsync();

            Assert.IsNotNull(users);
            Assert.That(users.Count() == 1);
        }



        // --- Get My Profile Publication TESTS ---
        [Test]
        public async Task GetMyProfilePubsShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IProfileService>();

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.DoesNotThrowAsync(async () => await service!.GetMyProfilePublicationsAsync(userId));

            var publications = await service!.GetMyProfilePublicationsAsync(userId);

            Assert.IsNotNull(publications);
            Assert.That(publications.Count() == 3);
        }

        [Test]
        public async Task GetMyProfilePubsShouldBeEmptyWithInvalidId()
        {
            var service = serviceProvider.GetService<IProfileService>();

            string userId = "InvalidUserId";

            var publications = await service!.GetMyProfilePublicationsAsync(userId);

            Assert.IsEmpty(publications);
        }



        // --- Get User Profile Publication TESTS ---
        [Test]
        public async Task GetUserProfilePubsShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IProfileService>();

            string username = "Pesho";

            Assert.DoesNotThrowAsync(async () => await service!.GetUserProfilePublicationsAsync(username));

            var publications = await service!.GetUserProfilePublicationsAsync(username);

            Assert.IsNotNull(publications);
            Assert.That(publications.Count() == 3);
        }

        [Test]
        public async Task GetUserProfilePubsShouldThrowWithInvalidId()
        {
            var service = serviceProvider.GetService<IProfileService>();

            string userId = "InvalidUserId";

            var publications = await service!.GetUserProfilePublicationsAsync(userId);

            Assert.IsEmpty(publications);
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
                ProfilePicture = new ProfilePicture()
                {
                    FileName = "",
                    Content = new byte[] { },
                    ImageToString = "",
                    UserId = ""
                },
            };

            var publication = new Publication()
            {
                Id = 1,
                Title = "TestPublication",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
                IsDeleted = false,
                UserId = user.Id,
                User = user,
                CategoryId = 1,
                Category = new Category()
                {
                    Name = "TestCategory",
                },
                PublicationsComments = new List<PublicationComment>(),
                UsersPublications = new List<UserPublication>(),
            };

            var publication5 = new Publication()
            {
                Id = 5,
                Title = "TestPublication",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
                IsDeleted = false,
                UserId = user.Id,
                User = user,
                CategoryId = 5,
                Category = new Category()
                {
                    Name = "TestCategory5",
                },
                PublicationsComments = new List<PublicationComment>(),
                UsersPublications = new List<UserPublication>(),
            };

            var publication10 = new Publication()
            {
                Id = 10,
                Title = "TestPublication10",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
                IsDeleted = false,
                UserId = user.Id,
                User = user,
                CategoryId = 2,
                Category = new Category()
                {
                    Name = "TestCategory10",
                },
                PublicationsComments = new List<PublicationComment>(),
                UsersPublications = new List<UserPublication>(),
            };

            await repo.AddAsync(user);
            await repo.AddAsync(publication);
            await repo.AddAsync(publication5);
            await repo.AddAsync(publication10);
            await repo.SaveChangesAsync();
        }
    }
}
