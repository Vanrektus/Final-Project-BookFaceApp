using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookFaceApp.Infrastructure.Data.Configuration
{
    internal class UserPublicationConfiguration : IEntityTypeConfiguration<UserPublication>
    {
        public void Configure(EntityTypeBuilder<UserPublication> builder)
        {
            builder
                .HasKey(ul => new { ul.UserId, ul.PublicationId });

            builder
                .HasOne<User>(up => up.User)
                .WithMany(u => u.UsersPublications)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<Publication>(up => up.Publication)
                .WithMany(p => p.UsersPublications)
                .HasForeignKey(p => p.PublicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
