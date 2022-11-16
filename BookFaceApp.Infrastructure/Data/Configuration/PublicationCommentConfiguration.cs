using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.Infrastructure.Data.Entities.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookFaceApp.Infrastructure.Data.Configuration
{
    internal class PublicationCommentConfiguration : IEntityTypeConfiguration<PublicationComment>
    {
        public void Configure(EntityTypeBuilder<PublicationComment> builder)
        {
            builder
                .HasKey(pc => new { pc.PublicationId, pc.CommentId });

            builder
                .HasOne<Publication>(pc => pc.Publication)
                .WithMany(p => p.PublicationsComments)
                .HasForeignKey(pc => pc.PublicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<Comment>(pc => pc.Comment)
                .WithMany(c => c.PublicationsComments)
                .HasForeignKey(pc => pc.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
