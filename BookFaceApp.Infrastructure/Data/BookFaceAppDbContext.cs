using BookFaceApp.Infrastructure.Data.Configuration;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Infrastructure.Data
{
    public class BookFaceAppDbContext : IdentityDbContext<User>
    {
        public BookFaceAppDbContext(DbContextOptions<BookFaceAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Publication> Publications { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<PublicationComment> PublicationsComments { get; set; } = null!;
        public DbSet<UserPublication> UsersPublications { get; set; } = null!;
        public DbSet<UserGroup> UsersGroups { get; set; } = null!;
        public DbSet<PublicationGroup> PublicationsGroups { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new PublicationCommentConfiguration());
            builder.ApplyConfiguration(new UserPublicationConfiguration());
            builder.ApplyConfiguration(new UserGroupConfiguration());
            builder.ApplyConfiguration(new PublicationGroupConfiguration());

            base.OnModelCreating(builder);
        }
    }
}