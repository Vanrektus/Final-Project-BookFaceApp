using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Comment;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.Extensions.DependencyInjection;

namespace BookFaceApp.Test.ServiceTests
{
    [TestFixture]
    public class CommentServiceTests
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
                .AddSingleton<ICommentService, CommentService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo!);
        }



        // --- Add Comment TESTS ---
        [Test]
        public async Task AddCommentShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<ICommentService>();

            var commentModel = new CommentAddModel()
            {
                Text = "Beautiful!",
            };

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.DoesNotThrowAsync(async () => await service!.AddCommentAsync(commentModel, 1, userId));

            var comment = await service!.GetCommentForEditAsync(4);

            Assert.IsNotNull(comment);
            Assert.IsTrue(comment.Id == 4);
        }



        // --- Delete Comment TESTS ---
        [Test]
        public async Task DeleteCommentShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<ICommentService>();
            var repo = serviceProvider.GetService<IRepository>();

            var commentId = 1;

            var commentBeforeDelete = await repo.GetByIdAsync<Comment>(commentId);

            Assert.IsNotNull(commentBeforeDelete);
            Assert.IsTrue(commentBeforeDelete.IsDeleted == false);

            await service!.DeleteCommentAsync(commentId);

            var commentAfterDelete = await repo.GetByIdAsync<Comment>(commentId);

            Assert.IsNotNull(commentAfterDelete);
            Assert.IsTrue(commentBeforeDelete.IsDeleted == true);
        }

        [Test]
        public void DeleteCommentShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<ICommentService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.DeleteCommentAsync(999));
        }



        // --- Edit Comment TESTS ---
        [Test]
        public async Task EditCommentShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<ICommentService>();
            var repo = serviceProvider.GetService<IRepository>();

            var commentId = 2;

            var commentBeforeEdit = await repo!.GetByIdAsync<Comment>(commentId);

            Assert.IsNotNull(commentBeforeEdit);
            Assert.IsTrue(commentBeforeEdit.Text == "Great");

            var commentEditModel = new CommentEditModel()
            {
                Id = commentId,
                Text = "great - edited"
            };

            await service!.EditCommentAsync(commentEditModel);

            var commentAfterDelete = await repo.GetByIdAsync<Comment>(commentId);

            Assert.IsNotNull(commentAfterDelete);
            Assert.IsTrue(commentBeforeEdit.Text == "great - edited");
        }

        [Test]
        public void EditCommentShouldThrowIfInvalidModelIsPassed()
        {
            var service = serviceProvider.GetService<CommentService>();

            var commentEditModel = new CommentEditModel();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.EditCommentAsync(commentEditModel));
        }



        // --- Comment Exists TESTS ---
        [Test]
        public async Task CommentExistsShouldWorkCorrectlyWithValidId()
        {
            var service = serviceProvider.GetService<ICommentService>();

            Assert.IsTrue(await service!.ExistsAsync(1));
        }

        [Test]
        public async Task CategoryExistsShouldWorkCorrectlyWithInvalidId()
        {
            var service = serviceProvider.GetService<ICommentService>();

            Assert.IsFalse(await service!.ExistsAsync(999));
        }



        // --- Is Comment Owner TESTS ---
        [Test]
        public async Task IsOwnerShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<ICommentService>();

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.IsTrue(await service!.IsOwnerAsync(1, userId));
        }

        [Test]
        public async Task IsOwnerShouldWorkCorrectlyWithInvalidData()
        {
            var service = serviceProvider.GetService<ICommentService>();

            string userId = "InvalidUserId";

            Assert.IsFalse(await service!.IsOwnerAsync(1, userId));
        }



        // --- Get Publication Id By Comment Id TESTS ---
        [Test]
        public async Task GetPubIdShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<ICommentService>();

            Assert.IsTrue(await service.GetPublicationIdByCommentIdAsync(2) == 1);
        }

        [Test]
        public async Task GetPubIdShouldWorkCorrectlyWithInvalidData()
        {
            var service = serviceProvider.GetService<ICommentService>();

            Assert.IsFalse(await service.GetPublicationIdByCommentIdAsync(1) == 2);
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

            var comment = new Comment()
            {
                Text = "Very nice comment!",
                UserId = user.Id,
                User = user,
            };

            var comment2 = new Comment()
            {
                Text = "Great",
                UserId = user.Id,
                User = user,
            };

            var comment3 = new Comment()
            {
                Text = "very nice",
                UserId = user.Id,
                User = user,
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

            var publicationComments = new List<PublicationComment>()
            {
                new PublicationComment()
                {
                    PublicationId = publication.Id,
                    Publication = publication,
                    CommentId = comment.Id,
                    Comment = comment
                },
                new PublicationComment()
                {
                    PublicationId = publication.Id,
                    Publication = publication,
                    CommentId = comment2.Id,
                    Comment = comment2
                },
                new PublicationComment()
                {
                    PublicationId = publication.Id,
                    Publication = publication,
                    CommentId = comment3.Id,
                    Comment = comment3
                },
            };

            publication.PublicationsComments = publicationComments;

            await repo.AddAsync(user);
            await repo.AddAsync(comment);
            await repo.AddAsync(comment2);
            await repo.AddAsync(comment3);
            await repo.AddAsync(publication);
            await repo.SaveChangesAsync();
        }
    }
}
