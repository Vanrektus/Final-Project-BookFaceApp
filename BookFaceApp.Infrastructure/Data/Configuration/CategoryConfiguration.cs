using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookFaceApp.Infrastructure.Data.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private List<Category> SeedCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Fun"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Animals"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Cars"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Politics"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Games"
                }
            };

            return categories;
        }
    }
}
