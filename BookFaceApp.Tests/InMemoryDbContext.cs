using BookFaceApp.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BookFaceApp.Tests
{
    public class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<BookFaceAppDbContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<BookFaceAppDbContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new BookFaceAppDbContext(dbContextOptions);

            context.Database.EnsureCreated();
        }

        public BookFaceAppDbContext CreateContext() => new BookFaceAppDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();
    }
}
