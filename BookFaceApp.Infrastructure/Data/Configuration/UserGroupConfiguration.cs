using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookFaceApp.Infrastructure.Data.Configuration
{
    internal class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            builder
                .HasOne<User>(ug => ug.User)
                .WithMany(u => u.UsersGroups)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<Group>(ug => ug.Group)
                .WithMany(u => u.UsersGroups)
                .HasForeignKey(ug => ug.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
