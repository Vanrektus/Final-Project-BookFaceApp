using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Group;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.Extensions.DependencyInjection;

namespace BookFaceApp.Test.ServiceTests
{
    public class GroupServiceTests
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
                .AddSingleton<IGroupService, GroupService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo!);
        }



        // --- Add Group TESTS ---
        [Test]
        public async Task AddGroupShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            var groupModel = new GroupAddModel()
            {
                Name = "TestGroup",
                CategoryId = 1
            };

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.DoesNotThrowAsync(async () => await service!.AddGroupAsync(groupModel, userId));

            var group = await service!.GetOneGroupAsync(3);

            Assert.IsNotNull(group);
            Assert.IsTrue(group.Id == 3);
        }



        // --- Delete Group TESTS ---
        [Test]
        public async Task DeleteGroupShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();
            var repo = serviceProvider.GetService<IRepository>();

            var groupId = 1;

            var groupBeforeDelete = await repo!.GetByIdAsync<Infrastructure.Data.Entities.Group>(groupId);

            Assert.IsNotNull(groupBeforeDelete);
            Assert.That(groupBeforeDelete.IsDeleted == false);

            await service!.DeleteGroupAsync(groupId);

            var groupAfterDelete = await repo!.GetByIdAsync<Infrastructure.Data.Entities.Group>(groupId);

            Assert.IsNotNull(groupAfterDelete);
            Assert.That(groupAfterDelete.IsDeleted == true);
        }

        [Test]
        public void DeleteGroupShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.DeleteGroupAsync(999));
        }



        // --- Edit Group TESTS ---
        [Test]
        public async Task EditGroupShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            var pubBeforeEdit = await service!.GetOneGroupAsync(1);

            Assert.IsTrue(pubBeforeEdit.Name == "TestGroup");

            var groupModel = new GroupEditModel()
            {
                Id = 1,
                Name = "TestGroupEdited",
                CategoryId = 1,
            };

            await service!.EditGroupAsync(groupModel);

            var pubAfterEdit = await service!.GetOneGroupAsync(1);

            Assert.IsTrue(pubAfterEdit.Name == "TestGroupEdited");
        }

        [Test]
        public void EditGroupShouldThrowIfInvalidModelIsPassed()
        {
            var service = serviceProvider.GetService<IGroupService>();

            var groupModel = new GroupEditModel();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.EditGroupAsync(groupModel));
        }



        // --- Get All Groups TESTS ---
        [Test]
        public async Task GetAllGroupsShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.DoesNotThrowAsync(async () => await service!.GetAllGroupsAsync());

            var groups = await service!.GetAllGroupsAsync();

            var expectedGroupsCount = 2;

            var actualPublicationsCount = groups.TotalGroupsCount;

            Assert.IsTrue(actualPublicationsCount == expectedGroupsCount);
        }



        // --- Get Group For Edit TESTS ---
        [Test]
        public async Task GetGroupForEditShouldNowThrow()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.DoesNotThrowAsync(async () => await service!.GetGroupForEditAsync(2));

            var group = await service!.GetGroupForEditAsync(2);

            Assert.IsNotNull(group);
            Assert.IsTrue(group.Id == 2);
        }

        [Test]
        public void GetGroupForEditShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.GetGroupForEditAsync(999));
        }



        // --- Get One Group TESTS ---
        [Test]
        public async Task GetOneGroupShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.DoesNotThrowAsync(async () => await service!.GetOneGroupAsync(2));

            var group = await service!.GetOneGroupAsync(2);

            Assert.IsNotNull(group);
            Assert.IsTrue(group.Id == 2);
        }

        [Test]
        public void GetOneGroupShouldThrowIfInvalidIdIsPassed()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.GetOneGroupAsync(999));
        }



        // --- Groups Exists TESTS ---
        [Test]
        public async Task GroupExistsShouldWorkCorrectlyWithValidId()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.IsTrue(await service!.ExistsByIdAsync(2));
        }

        [Test]
        public async Task GroupExistsShouldWorkCorrectlyWithInvalidId()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.IsFalse(await service!.ExistsByIdAsync(999));
        }



        // --- Is Group Owner TESTS ---
        [Test]
        public async Task IsOwnerShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<IGroupService>();

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.IsTrue(await service!.IsOwnerAsync(1, userId));
        }

        [Test]
        [TestCase(999, "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5")]
        [TestCase(999, "InvalidUserId")]
        public void IsOwnerShouldThrowWithInvalidData(int groupId, string userId)
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.IsOwnerAsync(groupId, userId));
        }



        // --- Request To Join TESTS ---
        [Test]
        public async Task RequestToJoinShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<IGroupService>();
            var repo = serviceProvider.GetService<IRepository>();

            int groupId = 1;
            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            await service!.RequestToJoin(groupId, userId);

            var user = await repo!.GetByIdAsync<User>(userId);

            var userGroup = user.UsersGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);

            Assert.IsNotNull(userGroup);
            Assert.IsTrue(userGroup.IsAccepted == false);
        }

        [Test]
        [TestCase(999, "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5")]
        [TestCase(999, "InvalidUserId")]
        public void RequestToJoinShouldThrowWithInvalidData(int groupId, string userId)
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.RequestToJoin(groupId, userId));
        }



        // --- Add User To Group TESTS ---
        [Test]
        public async Task AddUserToGroupShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<IGroupService>();
            var repo = serviceProvider.GetService<IRepository>();

            int groupId = 1;
            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            await service!.AddUserToGroup(groupId, userId);

            var user = await repo!.GetByIdAsync<User>(userId);

            var userGroup = user.UsersGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);

            Assert.IsNotNull(userGroup);
            Assert.IsTrue(userGroup.IsAccepted == true);
        }

        [Test]
        [TestCase(999, "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5")]
        [TestCase(999, "InvalidUserId")]
        public void AddUserToGroupShouldThrowWithInvalidData(int groupId, string userId)
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.RequestToJoin(groupId, userId));
        }



        // --- Remove User From Group TESTS ---
        [Test]
        public async Task RemoveUserFromGroupShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<IGroupService>();
            var repo = serviceProvider.GetService<IRepository>();

            int groupId = 1;
            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            await service!.RemoveUserFromGroup(groupId, userId);

            var user = await repo!.GetByIdAsync<User>(userId);

            var userGroup = user.UsersGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);

            Assert.IsNull(userGroup);
        }

        [Test]
        [TestCase(999, "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5")]
        [TestCase(999, "InvalidUserId")]
        public void RemoveUserFromGroupShouldThrowWithInvalidData(int groupId, string userId)
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.RequestToJoin(groupId, userId));
        }



        // --- Is User Accepted TESTS ---
        [Test]
        public async Task IsUserAcceptedShouldWorkCorrectlyWithValidData()
        {
            var service = serviceProvider.GetService<IGroupService>();
            var repo = serviceProvider.GetService<IRepository>();

            int groupId = 1;
            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            await service!.AddUserToGroup(groupId, userId);

            Assert.IsTrue(await service!.IsAccepted(groupId, userId));
        }

        [Test]
        [TestCase(999, "InvalidUserId")]
        public void IsUserAcceptedShouldThrowWithInvalidData(int groupId, string userId)
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.ThrowsAsync<NullReferenceException>(async () => await service!.IsAccepted(groupId, userId));
        }



        // --- Get All Unaccepted Users TESTS ---
        [Test]
        public async Task GetAllUnacceptedUsersShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            Assert.DoesNotThrowAsync(async () => await service!.GetAllUnacceptedUsers());

            var unacceptedUsers = await service!.GetAllUnacceptedUsers();

            var expectedUsersCount = 1;

            var actualPublicationsCount = unacceptedUsers.Count();

            Assert.IsTrue(actualPublicationsCount == expectedUsersCount);
        }



        // --- Is User In Group TESTS ---
        [Test]
        public async Task IsUserInGroupShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            int groupId = 1;
            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.IsFalse(await service!.IsUserInGroup(groupId, userId));

            await service!.AddUserToGroup(groupId, userId);

            Assert.IsTrue(await service!.IsUserInGroup(groupId, userId));
        }



        // --- Is User Requested TESTS ---
        [Test]
        public async Task IsUserRequestedShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IGroupService>();

            int groupId = 1;
            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            Assert.IsTrue(await service!.IsUserRequested(groupId, userId));

            await service!.RequestToJoin(groupId, userId);

            Assert.IsFalse(await service!.IsUserInGroup(groupId, userId));
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

            var group = new Infrastructure.Data.Entities.Group()
            {
                Name = "TestGroup",
                UserId = user.Id,
                User = user,
                CategoryId = 1,
                UsersGroups = new List<UserGroup>(),
                Publications = new List<Publication>()
            };

            group.UsersGroups = new List<UserGroup>()
            {
                new UserGroup()
                {
                    UserId = user.Id,
                    User = user,
                    GroupId = group.Id,
                    Group = group,
                }
            };

            var group2 = new Infrastructure.Data.Entities.Group()
            {
                Name = "TestGroup2",
                UserId = user.Id,
                User = user,
                CategoryId = 2,
                UsersGroups = new List<UserGroup>(),
                Publications = new List<Publication>()
            };

            await repo.AddAsync(user);
            await repo.AddAsync(group);
            await repo.AddAsync(group2);
            await repo.SaveChangesAsync();
        }
    }
}
