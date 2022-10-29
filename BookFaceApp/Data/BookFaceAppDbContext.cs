using BookFaceApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Data
{
    public class BookFaceAppDbContext : IdentityDbContext<User>
    {
        public BookFaceAppDbContext(DbContextOptions<BookFaceAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Publication> Publications { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<PublicationComment> PublicationsComments { get; set; } = null!;
        public DbSet<UserPublication> UsersPublications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PublicationComment>()
                .HasKey(pc => new { pc.PublicationId, pc.CommentId });

            builder.Entity<PublicationComment>()
                .HasOne<Publication>(pc => pc.Publication)
                .WithMany(p => p.PublicationsComments)
                .HasForeignKey(pc => pc.PublicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PublicationComment>()
                .HasOne<Comment>(pc => pc.Comment)
                .WithMany(c => c.PublicationsComments)
                .HasForeignKey(pc => pc.CommentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserPublication>()
                .HasKey(ul => new { ul.UserId, ul.PublicationId });

            builder.Entity<UserPublication>()
                .HasOne<User>(up => up.User)
                .WithMany(u => u.UsersPublications)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserPublication>()
                .HasOne<Publication>(up => up.Publication)
                .WithMany(p => p.UsersPublications)
                .HasForeignKey(p => p.PublicationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}