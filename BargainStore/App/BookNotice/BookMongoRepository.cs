using App.Domain.BookNotice;
using App.Domain.Infrastructure;
using App.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace App.BookNotice;

public class BookMongoRepository : IBookRepository
{
    private readonly IMongoCollection<BookDocument> _books;
    private readonly ILogger<BookMongoRepository> _logger;

    public BookMongoRepository(
        IOptions<MongoDbSettings> settings,
        IMongoClient mongoClient,
        ILogger<BookMongoRepository> logger)
    {
        _logger = logger;
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _books = database.GetCollection<BookDocument>(settings.Value.CollectionName);
    }

    public async Task<bool> IsExistsAsync(BookId id)
    {
        _logger.LogInformation("Checking if book with ID {} exists", id);
        var filter = Builders<BookDocument>.Filter.Eq(b => b.Id, id.ToString());
        var count = await _books.CountDocumentsAsync(filter);
        return count > 0;
    }

    public async Task<Book> LoadAsync(BookId id)
    {
        _logger.LogInformation("Loading book with ID {}", id);
        var filter = Builders<BookDocument>.Filter.Eq(b => b.Id, id.ToString());
        var bookDoc = await _books.Find(filter).FirstOrDefaultAsync();
        return bookDoc.ToDomain();
    }

    public async Task SaveAsync(Book entity)
    {
        _logger.LogInformation("Saving book with ID {}", entity.Id);
        var filter = Builders<BookDocument>.Filter.Eq(b => b.Id, entity.Id.ToString());
        var options = new ReplaceOptions { IsUpsert = true };

        var document = entity.ToDocument();
        await _books.ReplaceOneAsync(filter, document, options);
    }
}
