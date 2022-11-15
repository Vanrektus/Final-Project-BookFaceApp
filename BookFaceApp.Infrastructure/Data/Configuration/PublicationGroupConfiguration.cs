using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookFaceApp.Infrastructure.Data.Configuration
{
    internal class PublicationGroupConfiguration : IEntityTypeConfiguration<PublicationGroup>
    {
        public void Configure(EntityTypeBuilder<PublicationGroup> builder)
        {
            builder
                .HasKey(pg => new { pg.PublicationId, pg.GroupId });

            builder
                .HasOne<Publication>(pg => pg.Publication)
                .WithMany(u => u.PublicationsGroups)
                .HasForeignKey(pg => pg.PublicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<Group>(pg => pg.Group)
                .WithMany(u => u.PublicationsGroups)
                .HasForeignKey(pg => pg.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
