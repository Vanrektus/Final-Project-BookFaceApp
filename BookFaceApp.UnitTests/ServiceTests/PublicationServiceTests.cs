using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Publication;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.Extensions.DependencyInjection;

namespace BookFaceApp.Test.ServiceTests
{
    [TestFixture]
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



        // --- Add Publication TESTS ---
        [Test]
        public async Task AddPublicationShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var publicationModel = new PublicationAddModel()
            {
                Title = "TestPublication2",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
                CategoryId = 1,
            };

            int expectedReturnId = 11;

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            var actualReturnId = await service.AddPublicationAsync(publicationModel, userId);

            Assert.That(actualReturnId, Is.EqualTo(expectedReturnId));
            Assert.DoesNotThrowAsync(async () => await service.AddPublicationAsync(publicationModel, userId));
        }



        // --- Edit Publication TESTS ---
        [Test]
        public async Task EditPublicationShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var pubBeforeEdit = await service.GetOnePublicationAsync(1);

            Assert.IsTrue(pubBeforeEdit.Title == "TestPublication");

            var publicationModel = new PublicationEditModel()
            {
                Id = 1,
                Title = "TestTitle",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
                CategoryId = 1,
            };

            await service.EditPublicationAsync(publicationModel);

            var pubAfterEdit = await service.GetOnePublicationAsync(1);

            Assert.IsTrue(pubAfterEdit.Title == "TestTitle");
        }

        [Test]
        public void EditPublicationShouldThrowIfInvalidModelIsPassed()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var publicationModel = new PublicationEditModel();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.EditPublicationAsync(publicationModel));
        }



        // --- Get All Publications TESTS ---
        [Test]
        public async Task GetAllPublicationsShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.DoesNotThrowAsync(async () => await service.GetAllPublicationsAsync());

            var publications = await service.GetAllPublicationsAsync();
            var expectedPublicationsCount = 3;
            var actualPublicationsCount = publications.TotalPublicationsCount;

            Assert.IsTrue(actualPublicationsCount == expectedPublicationsCount);
        }



        // --- Get One Publication TESTS ---
        [Test]
        public async Task GetOnePubShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.DoesNotThrowAsync(async () => await service.GetOnePublicationAsync(10));

            var publication = await service.GetOnePublicationAsync(10);

            Assert.IsNotNull(publication);
            Assert.IsTrue(publication.Id == 10);
        }

        [Test]
        public void GetOnePubShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetOnePublicationAsync(999));
        }



        // --- Get Publication For Edit TESTS ---
        [Test]
        public async Task GetPublicationForEditShouldNowThrow()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.DoesNotThrowAsync(async () => await service.GetPublicationForEditAsync(1));

            var publication = await service.GetPublicationForEditAsync(10);

            Assert.IsNotNull(publication);
            Assert.IsTrue(publication.Id == 10);
        }

        [Test]
        public void GetPublicationForEditShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetPublicationForEditAsync(999));
        }



        // --- Like Publication TESTS ---
        [Test]
        public void LikePublicationShouldWorkCorrectlyWhenLiking()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.DoesNotThrowAsync(async () => await service.LikePublicationAsync(1, userId));
        }

        [Test]
        public async Task LikePublicationShouldWorkCorrectlyWhenUnliking()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            await service!.LikePublicationAsync(1, userId);

            Assert.DoesNotThrowAsync(async () => await service.LikePublicationAsync(1, userId));
        }

        [Test]
        [TestCase(1, "InvalidUserId")]
        [TestCase(999, "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5")]
        [TestCase(999, "InvalidUserId")]
        public void LikePublicationShouldThrowIfInvalidIdsArePassed(int publicationId, string userId)
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.LikePublicationAsync(publicationId, userId));
        }



        // --- Delete Publication TESTS ---
        [Test]
        public async Task DeletePublicationShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var publicationId = 1;

            var publicationBeforeDelete = await service!.GetOnePublicationAsync(publicationId);

            Assert.IsNotNull(publicationBeforeDelete);

            await service.DeletePublicationAsync(publicationId);

            Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetOnePublicationAsync(publicationId));
        }

        [Test]
        public void DeletePublicationShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.DeletePublicationAsync(999));
        }



        // --- Random Publications TESTS ---
        [Test]
        public async Task GetRandomPubsShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            var publications = await service!.GetRandomPublications();

            Assert.IsNotNull(publications);
            Assert.IsTrue(publications.Count() == 3);
        }



        // --- Category Exists TESTS ---
        [Test]
        public async Task CategoryExistsShouldWorkCorrectlyWithValidId()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.IsTrue(await service.CategoryExistsAsync(1));
        }

        [Test]
        public async Task CategoryExistsShouldWorkCorrectlyWithInvalidId()
        {
            var service = serviceProvider.GetService<IPublicationService>();

            Assert.IsFalse(await service.CategoryExistsAsync(999));
        }



        // --- PublicationCategory Matches GroupCategory TESTS ---
        [Test]
        public void PubCatMatchesGroupCatShouldWorkCorrectlyWithMatchingIds()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsTrue(publicationService.PublicationCatMatchesGroupCat(1, 1));
        }

        [Test]
        public void PubCatMatchesGroupCatShouldWorkCorrectlyWithNotMatchingIds()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsFalse(publicationService.PublicationCatMatchesGroupCat(1, 999));
        }



        // --- Publication Exists TESTS ---
        [Test]
        public async Task PublicationExistsShouldWorkCorrectlyWithValidId()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsTrue(await publicationService.ExistsAsync(1));
        }

        [Test]
        public async Task PublicationExistsShouldWorkCorrectlyWithInvalidId()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsFalse(await publicationService.ExistsAsync(999));
        }


        // --- Is Publication Owner TESTS ---
        [Test]
        public async Task IsOwnerShouldWorkCorrectlyWithValidData()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.IsTrue(await publicationService.IsOwnerAsync(1, userId));
        }

        [Test]
        public async Task IsOwnerShouldWorkCorrectlyWithInvalidData()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            string userId = "InvalidUserId";

            Assert.IsFalse(await publicationService.IsOwnerAsync(1, userId));
        }



        // --- Is Publication In Group TESTS ---
        [Test]
        public async Task IsPubInGroupShouldWorkCorrectlyWithValidData()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsTrue(await publicationService.IsInGroupAsync(2));
        }

        [Test]
        public async Task IsPubInGroupShouldWorkCorrectlyWithInvalidData()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsFalse(await publicationService.IsInGroupAsync(1));
        }



        // --- Get Publication Group Id TESTS ---
        [Test]
        public async Task GetPubGroupIdShouldWorkCorrectlyWithValidData()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsTrue(await publicationService.GetPublicationGroupIdAsync(2) == 1);
        }

        [Test]
        public async Task GetPubGroupIdShouldWorkCorrectlyWithInvalidData()
        {
            var publicationService = serviceProvider.GetService<IPublicationService>();

            Assert.IsFalse(await publicationService.GetPublicationGroupIdAsync(1) == 1);
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

            var group = new Group()
            {
                Id = 1,
                Name = "TestGroup",
                CategoryId = 1,
                UserId = user.Id,
            };

            var publicationInGroup = new Publication()
            {
                Id = 2,
                Title = "TestPublication2",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/640px-Image_created_with_a_mobile_phone.png",
                IsDeleted = false,
                UserId = user.Id,
                User = user,
                GroupId = 1,
                Group = group,
                CategoryId = 1,
                Category = new Category()
                {
                    Name = "TestCategory",
                },
                PublicationsComments = new List<PublicationComment>(),
                UsersPublications = new List<UserPublication>(),
            };


            await repo.AddAsync(user);
            await repo.AddAsync(publication);
            await repo.AddAsync(publication5);
            await repo.AddAsync(publication10);
            await repo.AddAsync(publicationInGroup);
            await repo.SaveChangesAsync();
        }
    }
}